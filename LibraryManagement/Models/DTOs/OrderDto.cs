using LibraryManagement.Models.Enums;

namespace LibraryManagement.Models.DTOs;

public record CreateCheckoutDto(Guid UserId, Guid BookFormatId, int Quantity = 1);

public record CheckoutResponseDto(Guid OrderId, string CheckoutUrl);

public record OrderItemDto(
    Guid BookId,
    Guid BookFormatId,
    string BookTitle,
    decimal UnitPrice,
    int Quantity
);

public record OrderDto(
    Guid Id,
    Guid UserId,
    OrderStatus Status,
    decimal TotalAmount,
    string Currency,
    DateTime? PaidAt,
    IReadOnlyCollection<OrderItemDto> Items
);
