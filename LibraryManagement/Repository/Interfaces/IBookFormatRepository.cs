namespace LibraryManagement.Repository.Interfaces;
using LibraryManagement.Models;

public interface IBookFormatRepository
{
    Task<IEnumerable<BookFormat>> GetAllBookFormatsAsync(CancellationToken cancellationToken = default);
    Task<BookFormat?> GetBookFormatByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddBookFormatAsync(BookFormat bookFormat, CancellationToken cancellationToken = default);
    Task UpdateBookFormatAsync(BookFormat bookFormat, CancellationToken cancellationToken = default);
    Task DeleteBookFormatAsync(BookFormat bookFormat, CancellationToken cancellationToken = default);
}