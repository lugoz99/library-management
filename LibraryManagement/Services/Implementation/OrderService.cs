using FluentResults;
using LibraryManagement.Helpers.Errors;
using LibraryManagement.Models;
using LibraryManagement.Models.DTOs;
using LibraryManagement.Models.Enums;
using LibraryManagement.Repository.Interfaces;
using LibraryManagement.Services.Contracts;
using MapsterMapper;
using Stripe;
using Stripe.Checkout;

namespace LibraryManagement.Services.Implementation;

public class OrderService(
    IOrderRepository orderRepository,
    IMapper mapper,
    IConfiguration configuration) : IOrderService
{
    public async Task<Result<CheckoutResponseDto>> CreateCheckoutAsync(
        CreateCheckoutDto dto, CancellationToken cancellationToken = default)
    {
        if (dto.Quantity is < 1 or > 10)
            return Result.Fail<CheckoutResponseDto>(
                new ValidationError(nameof(dto.Quantity), "must be between 1 and 10."));

        var user = await orderRepository.GetUserAsync(dto.UserId, cancellationToken);
        if (user is null)
            return Result.Fail<CheckoutResponseDto>(new NotFoundError(nameof(User), dto.UserId));

        var format = await orderRepository.GetBookFormatAsync(dto.BookFormatId, cancellationToken);
        if (format is null)
            return Result.Fail<CheckoutResponseDto>(
                new NotFoundError(nameof(BookFormat), dto.BookFormatId));
        if (!format.IsAvailable || format.Stock is 0)
            return Result.Fail<CheckoutResponseDto>(
                new ConflictError("The selected book format is not available."));

        var total = format.Price * dto.Quantity;
        var order = new Order
        {
            UserId = user.Id,
            TotalAmount = total,
            Items =
            [
                new OrderItem
                {
                    BookId = format.BookId,
                    BookFormatId = format.Id,
                    BookTitle = format.Book.Title,
                    UnitPrice = format.Price,
                    Quantity = dto.Quantity
                }
            ]
        };
        await orderRepository.AddAsync(order, cancellationToken);

        var secretKey = configuration["Stripe:SecretKey"]
                        ?? Environment.GetEnvironmentVariable("STRIPE__SECRET_KEY");
        if (string.IsNullOrWhiteSpace(secretKey))
            return Result.Fail<CheckoutResponseDto>(
                new InternalServerError("Stripe is not configured."));

        StripeConfiguration.ApiKey = secretKey;
        var options = new SessionCreateOptions
        {
            Mode = "payment",
            CustomerEmail = user.Email,
            SuccessUrl = configuration["Stripe:SuccessUrl"] ?? "http://localhost:3000/success",
            CancelUrl = configuration["Stripe:CancelUrl"] ?? "http://localhost:3000/cancel",
            ClientReferenceId = order.Id.ToString(),
            Metadata = new Dictionary<string, string> { ["order_id"] = order.Id.ToString() },
            LineItems =
            [
                new SessionLineItemOptions
                {
                    Quantity = dto.Quantity,
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = order.Currency,
                        UnitAmountDecimal = format.Price * 100,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = format.Book.Title
                        }
                    }
                }
            ]
        };

        var session = await new SessionService().CreateAsync(options, cancellationToken: cancellationToken);
        order.StripeCheckoutSessionId = session.Id;
        await orderRepository.UpdateAsync(order, cancellationToken);
        return Result.Ok(new CheckoutResponseDto(order.Id, session.Url));
    }

    public async Task<Result<OrderDto>> GetByIdAsync(
        Guid id, CancellationToken cancellationToken = default)
    {
        var order = await orderRepository.GetByIdAsync(id, cancellationToken);
        return order is null
            ? Result.Fail<OrderDto>(new NotFoundError(nameof(Order), id))
            : Result.Ok(mapper.Map<OrderDto>(order));
    }

    public async Task<Result> CompleteCheckoutAsync(
        string sessionId, string? paymentIntentId, CancellationToken cancellationToken = default)
    {
        var order = await orderRepository.GetByStripeSessionIdAsync(sessionId, cancellationToken);
        if (order is null)
            return Result.Fail(new NotFoundError(nameof(Order), sessionId));
        if (order.Status == OrderStatus.Paid)
            return Result.Ok();

        order.Status = OrderStatus.Paid;
        order.StripePaymentIntentId = paymentIntentId;
        order.PaidAt = DateTime.UtcNow;
        foreach (var item in order.Items)
        {
            if (item.BookFormat.Stock.HasValue)
                item.BookFormat.Stock -= item.Quantity;
        }
        await orderRepository.UpdateAsync(order, cancellationToken);
        return Result.Ok();
    }
}
