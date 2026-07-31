using LibraryManagement.Models;

namespace LibraryManagement.Repository.Interfaces;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAllAsync(CancellationToken cancellationToken=default);

    Task<Author?> GetByIdAsync(Guid id, CancellationToken cancellationToken=default);
    
    Task AddAsync(Author author, CancellationToken cancellationToken=default);
    
    Task UpdateAsync(Author author,CancellationToken cancellationToken=default);

    Task DeleteAsync(Author author, CancellationToken cancellationToken=default);

}