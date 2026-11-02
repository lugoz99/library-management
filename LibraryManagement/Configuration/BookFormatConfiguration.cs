using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Configuration;

public class BookFormatConfiguration:IEntityTypeConfiguration<BookFormat>
{
    public void Configure(EntityTypeBuilder<BookFormat> builder)
    {
        // Price Decimal Precision
        builder.Property(bf => bf.Price)
            .HasPrecision(18, 2);
        
        builder.Property(bf => bf.Weight)
            .HasPrecision(18, 2);
        
        // Enum conversion
        builder.Property(bf => bf.FormatType)
            .HasConversion<string>();
        
        
        builder.HasOne(bf => bf.Book)
            .WithMany(b => b.BooksFormats)
            .HasForeignKey(bf => bf.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(bf => bf.Edition).HasMaxLength(80);
    }
}