using LibraryManagement.Models;

namespace LibraryManagement.Repository.Interfaces;

public interface IPublisherRepository
{
    Task<IReadOnlyList<Publisher>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<Publisher?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task AddAsync(Publisher publisher, CancellationToken cancellationToken = default);
    
    void Update(Publisher publisher);
    
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);    Task SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default);
}