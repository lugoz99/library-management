namespace LibraryManagement.Repository.Interfaces;
using LibraryManagement.Models;

public interface IBookFormatRepository
{
    Task<IEnumerable<BookFormat>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<BookFormat?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(BookFormat bookFormat, CancellationToken cancellationToken = default);
    Task UpdateAsync(BookFormat bookFormat, CancellationToken cancellationToken = default);
    Task DeleteAsync(BookFormat bookFormat, CancellationToken cancellationToken = default);
}