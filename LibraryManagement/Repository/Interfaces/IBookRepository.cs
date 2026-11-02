using LibraryManagement.Models;

namespace LibraryManagement.Repository.Interfaces;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Book?>GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Book book, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<BookAuthors> booksAuthors, CancellationToken cancellationToken = default);
    //Task<Book> UpdateBookAsync(Book book, CancellationToken cancellationToken = default);
    Task DeleteAsync(Book book, CancellationToken cancellationToken = default);
    
    Task UpdateAsync(Book book, CancellationToken cancellationToken = default);
    
    Task<bool> AuthorRelationExistsAsync(Guid bookId, Guid authorId, CancellationToken ct = default);
    
}