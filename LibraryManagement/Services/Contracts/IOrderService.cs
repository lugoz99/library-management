using FluentResults;
using LibraryManagement.Models.DTOs;

namespace LibraryManagement.Services.Contracts;

public interface IOrderService
{
    Task<Result<CheckoutResponseDto>> CreateCheckoutAsync(
        CreateCheckoutDto dto, CancellationToken cancellationToken = default);
    Task<Result<OrderDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result> CompleteCheckoutAsync(
        string sessionId, string? paymentIntentId, CancellationToken cancellationToken = default);
}
