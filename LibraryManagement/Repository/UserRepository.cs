using LibraryManagement.Data;
using LibraryManagement.Models;
using LibraryManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository;

public class UserRepository(ApplicationDbContext context):IUserRepository
{
    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Users
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task Addsync(User user, CancellationToken cancelationToken = default)
    {
        await context.Users.AddAsync(user, cancelationToken);
        await context.SaveChangesAsync(cancelationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        context.Remove(user);
        await context.SaveChangesAsync(cancellationToken);
    }
}