using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Configuration;

public class WishListConfiguration:IEntityTypeConfiguration<WishList>
{
    public void Configure(EntityTypeBuilder<WishList> builder)
    {
        
        
        builder.Property(wl => wl.Title).IsRequired().HasMaxLength(200);
        builder.HasIndex(wl => wl.Title).IsUnique();
        // Relación uno-a-muchos: un usuario puede tener varias wishlists
        builder.HasOne(wl => wl.User)
            .WithMany() // o .WithMany(u => u.WishLists) si agregas la colección en User
            .HasForeignKey(wl => wl.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        
        
        
        builder.HasMany(wl => wl.Books)
            .WithMany(b => b.WishLists)
            .UsingEntity(j =>
            {
                j.ToTable("WishListItem");
                j.HasOne(typeof(Book)).WithMany().HasForeignKey("BookId");
                j.HasOne(typeof(WishList)).WithMany().HasForeignKey("WishListId");
                j.HasIndex("WishListId", "BookId").IsUnique();
            });
    }
}