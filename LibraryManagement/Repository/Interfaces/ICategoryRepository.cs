using LibraryManagement.Models;

namespace LibraryManagement.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        // Lectura
        Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        IQueryable<Category> GetAllQueryable();
        Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default);
    
        // Escritura
        Task AddAsync(Category category, CancellationToken cancellationToken = default);
        Task UpdateAsync(Category category, CancellationToken cancellationToken = default);
        Task DeleteAsync(Category category, CancellationToken cancellationToken = default);
    }
}