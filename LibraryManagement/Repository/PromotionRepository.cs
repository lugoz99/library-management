using LibraryManagement.Data;
using LibraryManagement.Models;
using LibraryManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository;

public class PromotionRepository(ApplicationDbContext context) : IPromotionRepository
{
    public async Task<IEnumerable<Promotion>> GetByBookIdAsync(
        Guid bookId, CancellationToken cancellationToken = default) =>
        await context.Promotions
            .Where(promotion => promotion.BookId == bookId)
            .OrderByDescending(promotion => promotion.StartsAt)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public Task<bool> BookExistsAsync(Guid bookId, CancellationToken cancellationToken = default) =>
        context.Books.AnyAsync(book => book.Id == bookId, cancellationToken);

    public Task<bool> HasOverlappingActivePromotionAsync(
        Guid bookId, DateTime startsAt, DateTime endsAt,
        CancellationToken cancellationToken = default) =>
        context.Promotions.AnyAsync(promotion =>
            promotion.BookId == bookId &&
            promotion.IsActive &&
            startsAt < promotion.EndsAt &&
            endsAt > promotion.StartsAt,
            cancellationToken);

    public async Task AddAsync(Promotion promotion, CancellationToken cancellationToken = default)
    {
        await context.Promotions.AddAsync(promotion, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public Task<Promotion?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        context.Promotions.FirstOrDefaultAsync(promotion => promotion.Id == id, cancellationToken);

    public async Task UpdateAsync(Promotion promotion, CancellationToken cancellationToken = default)
    {
        context.Promotions.Update(promotion);
        await context.SaveChangesAsync(cancellationToken);
    }
}
