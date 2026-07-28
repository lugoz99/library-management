using LibraryManagement.Data;
using LibraryManagement.Models;
using LibraryManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository;

public class PublisherRepository(ApplicationDbContext context):IPublisherRepository
{
    public async Task<IReadOnlyList<Publisher>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Publishers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Publisher?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Publishers
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task AddAsync(Publisher publisher, CancellationToken cancellationToken = default)
    {
        context.Publishers.Add(publisher);
        await context.SaveChangesAsync(cancellationToken);
    }

    public void Update(Publisher publisher)
    {
        context.Publishers.Update(publisher);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await context.Publishers
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default)
    {
        
        var query =  context.Publishers
            .AsNoTracking()
            .Where(p => p.Name == name);
        
        if(excludeId.HasValue)
        {
            query = query.Where(p => p.Id != excludeId.Value);
        }
        return await query.AnyAsync(cancellationToken);
    }
}