using Microsoft.EntityFrameworkCore;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Configuration;

public class BookAuthorConfiguration:IEntityTypeConfiguration<BookAuthors>
{
    public void Configure(EntityTypeBuilder<BookAuthors> builder)
    {
        builder.HasKey(ba => ba.Id);
        // Avoid duplication
        // one author cannot be twice in the same book
        builder.HasIndex(ba => new { ba.BookId, ba.AuthorId }).IsUnique();
        
        builder.Property(ba => ba.Role)
            .HasMaxLength(100);
        
        // each bookAuthor Entity has a book and a book has many ba
        builder.HasOne(ba => ba.Book)
            .WithMany(b => b.BookAuthors)
            .HasForeignKey(ba => ba.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ba => ba.Author)
            .WithMany(a => a.BookAuthors)
            .HasForeignKey(ba => ba.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}