using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Configuration;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {

        // Required field
        builder.Property(a => a.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        // Optional fields with string length constraints
        builder.Property(a => a.LastNames)
            .HasMaxLength(100);

        builder.Property(a => a.Biography)
            .HasMaxLength(2000);

        builder.Property(a => a.Nationality)
            .HasMaxLength(50);

        // Date fields (optional)
        builder.Property(a => a.DateOfBirth)
            .IsRequired(false);

        builder.Property(a => a.DateOfDeath)
            .IsRequired(false);
    }
}