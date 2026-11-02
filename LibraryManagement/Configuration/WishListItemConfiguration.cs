using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Configuration;

public class WishListItemConfiguration : IEntityTypeConfiguration<WishListItem>
{
    public void Configure(EntityTypeBuilder<WishListItem> builder)
    {
        builder.ToTable("WishListItems");
        builder.HasKey(item => new { item.WishListId, item.BookId });

        builder.HasOne(item => item.Book)
            .WithMany(book => book.WishListItems)
            .HasForeignKey(item => item.BookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
