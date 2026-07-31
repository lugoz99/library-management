using LibraryManagement.Data;
using LibraryManagement.Models;
using LibraryManagement.Models.DTOs;
using LibraryManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository;

public class AuthorRepository(ApplicationDbContext context):IAuthorRepository
{
    public async Task<IEnumerable<Author>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Authors
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Author?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Authors.
            AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task AddAsync(Author author, CancellationToken cancellationToken = default)
    {
        await context.Authors.AddAsync(author, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Author author, CancellationToken cancellationToken = default)
    {
        context.Authors.Update(author);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Author author, CancellationToken cancellationToken = default)
    {
        context.Authors.Remove(author);
        await context.SaveChangesAsync(cancellationToken);
    }
}