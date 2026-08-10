using LibraryManagement.Models;

namespace LibraryManagement.Repository.Interfaces;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Review review, CancellationToken cancellationToken = default);
}