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
            .WithMany(user => user.WishLists)
            .HasForeignKey(wl => wl.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        
        
        
        builder.HasMany(wl => wl.Items)
            .WithOne(item => item.WishList)
            .HasForeignKey(item => item.WishListId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}