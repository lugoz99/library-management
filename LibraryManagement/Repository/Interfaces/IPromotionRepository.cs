using LibraryManagement.Models;

namespace LibraryManagement.Repository.Interfaces;

public interface IPromotionRepository
{
    Task<IEnumerable<Promotion>> GetByBookIdAsync(Guid bookId, CancellationToken cancellationToken = default);
    Task<bool> BookExistsAsync(Guid bookId, CancellationToken cancellationToken = default);
    Task<bool> HasOverlappingActivePromotionAsync(
        Guid bookId, DateTime startsAt, DateTime endsAt,
        CancellationToken cancellationToken = default);
    Task AddAsync(Promotion promotion, CancellationToken cancellationToken = default);
    Task<Promotion?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateAsync(Promotion promotion, CancellationToken cancellationToken = default);
}
