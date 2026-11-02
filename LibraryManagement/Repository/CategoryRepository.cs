using LibraryManagement.Data;
using LibraryManagement.Models;
using LibraryManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository
{
    public class CategoryRepository(ApplicationDbContext context) : ICategoryRepository
    {
        #region Métodos de Lectura (Queries)

        public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await context.Categories
                .Include(c => c.SubCategories)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public IQueryable<Category> GetAllQueryable()
        {
            // AsNoTracking no recibe cancellationToken directamente porque devuelve un IQueryable diferido
            return context.Categories
                .AsNoTracking();
        }

        public async Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default)
        {
            var query = context.Categories
                .AsNoTracking()
                .Where(c => c.Name == name);

            if (excludeId.HasValue)
            {
                query = query.Where(c => c.Id != excludeId.Value);
            }

            return await query.AnyAsync(cancellationToken);
        }

        #endregion

        #region Métodos de Escritura (Commands)

        public async Task AddAsync(Category category, CancellationToken cancellationToken = default)
        {
            await context.Categories.AddAsync(category, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Category category, CancellationToken cancellationToken = default)
        {
            context.Categories.Update(category);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Category category, CancellationToken cancellationToken = default)
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync(cancellationToken);
        }

        #endregion
    }
}