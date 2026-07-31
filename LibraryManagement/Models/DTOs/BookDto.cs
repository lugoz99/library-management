using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using LibraryManagement.Models.Enums;

namespace LibraryManagement.Models.DTOs;

// ==================== BOOK ====================

[UsedImplicitly]
public class CreateBookDto
{
    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "ISBN is required.")]
    [MaxLength(13, ErrorMessage = "ISBN cannot be longer than 13 characters.")]
    public string Isbn { get; set; } = null!;

    [MaxLength(2000, ErrorMessage = "Description cannot be longer than 2000 characters.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Category is required.")]
    public Guid CategoryId { get; set; }

    [Required(ErrorMessage = "Publisher is required.")]
    public Guid PublisherId { get; set; }

    [Required(ErrorMessage = "Publication date is required.")]
    public DateOnly PublicationDate { get; set; }

    [Range(1, 10000, ErrorMessage = "Page count must be between 1 and 10000.")]
    public int PagesCount { get; set; }

    [Required(ErrorMessage = "Language is required.")]
    [MaxLength(10, ErrorMessage = "Language cannot be longer than 10 characters.")]
    public string Language { get; set; } = null!;

    public bool IsAvailable { get; set; } = true;
}

[UsedImplicitly]
public class UpdateBookDto
{
    [MaxLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
    public string? Title { get; set; }

    [MaxLength(13, ErrorMessage = "ISBN cannot be longer than 13 characters.")]
    public string? Isbn { get; set; }

    [MaxLength(2000, ErrorMessage = "Description cannot be longer than 2000 characters.")]
    public string? Description { get; set; }

    public Guid? CategoryId { get; set; }

    public Guid? PublisherId { get; set; }

    public DateOnly? PublicationDate { get; set; }

    [Range(1, 10000, ErrorMessage = "Page count must be between 1 and 10000.")]
    public int? PagesCount { get; set; }

    [MaxLength(10, ErrorMessage = "Language cannot be longer than 10 characters.")]
    public string? Language { get; set; }

    public bool? IsAvailable { get; set; }
}

[UsedImplicitly]
public class BookDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Isbn { get; set; } = null!;
    public bool IsAvailable { get; set; }
    public string? Description { get; set; }
    public string? CoverImageUrl { get; set; }
    public Guid CategoryId { get; set; }
    public Guid PublisherId { get; set; }
    public DateOnly? PublicationDate { get; set; }
    public int PagesCount { get; set; }
    public string Language { get; set; } = null!;
}

// ==================== BOOK FORMAT ====================

[UsedImplicitly]
public class CreateBookFormatDto
{
    [Required(ErrorMessage = "Book is required.")]
    public Guid BookId { get; set; }

    [Required(ErrorMessage = "Format type is required.")]
    [EnumDataType(typeof(FormatType), ErrorMessage = "Invalid format type.")]
    public string FormatType { get; set; } = null!;

    [Required(ErrorMessage = "Price is required.")]
    [Range(100, 10_000_000, ErrorMessage = "Price must be between 100 and 10000000.")]
    public decimal Price { get; set; }

    [Range(0, 100, ErrorMessage = "Weight must be between 0 and 100.")]
    public decimal? Weight { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
    public int? Stock { get; set; }

    [MaxLength(50, ErrorMessage = "Edition cannot be longer than 50 characters.")]
    public string? Edition { get; set; }

    public bool IsAvailable { get; set; } = true;
}

[UsedImplicitly]
public class UpdateBookFormatDto
{
    [EnumDataType(typeof(FormatType), ErrorMessage = "Invalid format type.")]
    public string? FormatType { get; set; }

    [Range(100, 10_000_000, ErrorMessage = "Price must be between 100 and 10000000.")]
    public decimal? Price { get; set; }

    [Range(0, 100, ErrorMessage = "Weight must be between 0 and 100.")]
    public decimal? Weight { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
    public int? Stock { get; set; }

    [MaxLength(50, ErrorMessage = "Edition cannot be longer than 50 characters.")]
    public string? Edition { get; set; }

    public bool? IsAvailable { get; set; }
}

[UsedImplicitly]
public class BookFormatDto
{
    public Guid Id { get; set; }
    public string? FormatType { get; set; }
    public decimal Price { get; set; }
    public decimal? Weight { get; set; }
    public bool IsAvailable { get; set; }
    public int? Stock { get; set; }
    public Guid BookId { get; set; }
    public string? KeyUrl { get; set; }
    public string? Edition { get; set; }
    public bool IsDigital => FormatType is "Ebook" or "Pdf";
}