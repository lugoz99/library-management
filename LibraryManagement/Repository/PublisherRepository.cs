using LibraryManagement.Data;
using LibraryManagement.Models;
using LibraryManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository;

public class PublisherRepository(ApplicationDbContext context) : IPublisherRepository
{
    // Gets a list of all publishers in the database
    public async Task<IEnumerable<Publisher>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Publishers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    // Finds a specific publisher by its unique identifier, or returns null if not found
    public async Task<Publisher?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Publishers
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    // Adds a new publisher and saves changes immediately
    public async Task AddAsync(Publisher publisher, CancellationToken cancellationToken = default)
    {
        await context.Publishers.AddAsync(publisher, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    // Updates an existing publisher and saves changes immediately
    public async Task UpdateAsync(Publisher publisher, CancellationToken cancellationToken = default)
    {
        context.Publishers.Update(publisher);
        await context.SaveChangesAsync(cancellationToken);
    }

    // Removes the publisher and saves changes immediately
    public async Task DeleteAsync(Publisher publisher, CancellationToken cancellationToken = default)
    {
        context.Publishers.Remove(publisher);
        await context.SaveChangesAsync(cancellationToken);
    }

    // Checks if a publisher with this name already exists, optionally ignoring a specific ID during updates
    public async Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default)
    {
        var query = context.Publishers
            .AsNoTracking()
            .Where(p => p.Name == name);
        
        if (excludeId.HasValue)
        {
            query = query.Where(p => p.Id != excludeId.Value);
        }
        
        return await query.AnyAsync(cancellationToken);
    }
}