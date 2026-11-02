using LibraryManagement.Data;
using LibraryManagement.Models;
using LibraryManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository;

public class OrderRepository(ApplicationDbContext context) : IOrderRepository
{
    public Task<User?> GetUserAsync(Guid id, CancellationToken cancellationToken = default) =>
        context.Users.FirstOrDefaultAsync(user => user.Id == id, cancellationToken);

    public Task<BookFormat?> GetBookFormatAsync(Guid id, CancellationToken cancellationToken = default) =>
        context.BookFormats.Include(format => format.Book)
            .FirstOrDefaultAsync(format => format.Id == id, cancellationToken);

    public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        await context.Orders.AddAsync(order, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        context.Orders.Include(order => order.Items)
            .AsNoTracking()
            .FirstOrDefaultAsync(order => order.Id == id, cancellationToken);

    public Task<Order?> GetByStripeSessionIdAsync(
        string sessionId, CancellationToken cancellationToken = default) =>
        context.Orders.Include(order => order.Items)
            .ThenInclude(item => item.BookFormat)
            .FirstOrDefaultAsync(
            order => order.StripeCheckoutSessionId == sessionId, cancellationToken);

    public async Task UpdateAsync(Order order, CancellationToken cancellationToken = default)
    {
        context.Orders.Update(order);
        await context.SaveChangesAsync(cancellationToken);
    }
}
