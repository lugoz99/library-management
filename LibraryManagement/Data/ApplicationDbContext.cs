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
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<User> Users { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Category>(entity =>
            {
                // Configure the self-referencing relationship (1 Parent -> Many Subcategories)
                entity.HasOne(c => c.ParentCategory)              // 1. Navigation property to Parent
                    .WithMany(c => c.SubCategories)             // 2. Navigation collection for Children
                    .HasForeignKey(c => c.ParentCategoryId)     // 3. Foreign Key property (ID) , it's no necessary
                    .OnDelete(DeleteBehavior.Restrict);         // 4. Prevent deleting parent if it has children
            });

            modelBuilder.Entity<Publisher>()
                .HasIndex(p => p.Name)
                .IsUnique();
        }

        public override Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
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