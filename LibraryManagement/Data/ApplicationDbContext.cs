using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data
{
        public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : DbContext(options)
        {
            public DbSet<Category> Categories { get; set; }
            public DbSet<Publisher> Publishers { get; set; }
            public DbSet<Author> Authors { get; set; }
            
            public DbSet<BookAuthors> BookAuthors { get; set; }
            public DbSet<Book> Books { get; set; }
            
            public DbSet<BookFormat> BookFormats { get; set; }

            
            public DbSet<Review> Reviews { get; set; }
            
            
            public DbSet<WishList> WishLists { get; set; }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // creador de modelos
                base.OnModelCreating(modelBuilder);

                // Esto busca automáticamente todas las clases que implementen IEntityTypeConfiguration en este ensamblado!
                modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            }

            public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            {
                foreach (var entry in ChangeTracker.Entries<BaseEntity>())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.CreatedAt = DateTime.UtcNow;
                            break;
                        case EntityState.Modified:
                            entry.Entity.UpdatedAt = DateTime.UtcNow;
                            break;
                    }
                }

                return base.SaveChangesAsync(cancellationToken);
            }
        }
}