using LibraryManagement.Models;

namespace LibraryManagement.Repository.Interfaces;

public interface IOrderRepository
{
    Task<User?> GetUserAsync(Guid id, CancellationToken cancellationToken = default);
    Task<BookFormat?> GetBookFormatAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Order order, CancellationToken cancellationToken = default);
    Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Order?> GetByStripeSessionIdAsync(string sessionId, CancellationToken cancellationToken = default);
    Task UpdateAsync(Order order, CancellationToken cancellationToken = default);
}
