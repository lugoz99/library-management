using LibraryManagement.Models;

namespace LibraryManagement.Repository.Interfaces;

public interface IPublisherRepository
{
    // Gets a list of all publishers in the database
    Task<IEnumerable<Publisher>> GetAllAsync(CancellationToken cancellationToken = default);
    
    // Finds a specific publisher by its unique identifier, or returns null if not found
    Task<Publisher?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    // Adds a new publisher and saves changes immediately
    Task AddAsync(Publisher publisher, CancellationToken cancellationToken = default);
    
    // Updates an existing publisher and saves changes immediately
    Task UpdateAsync(Publisher publisher, CancellationToken cancellationToken = default);
    
    // Removes the publisher and saves changes immediately
    Task DeleteAsync(Publisher publisher, CancellationToken cancellationToken = default);    

    // Checks if a publisher with this name already exists, optionally ignoring a specific ID during updates
    Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default);
}