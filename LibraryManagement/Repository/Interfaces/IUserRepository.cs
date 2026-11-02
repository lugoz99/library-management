using LibraryManagement.Models;

namespace LibraryManagement.Repository.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default);
    Task Addsync(User user, CancellationToken cancelationToken = default);
    Task<User?> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
    Task DeleteAsync(User user,CancellationToken cancellationToken = default);
}