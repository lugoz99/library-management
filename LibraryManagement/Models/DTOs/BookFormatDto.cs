using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using LibraryManagement.Models.Enums;

namespace LibraryManagement.Models.DTOs;

[UsedImplicitly]
public record CreateBookFormatDto(
    [Required(ErrorMessage = "Book is required.")]
    Guid BookId,

    [Required(ErrorMessage = "Format type is required.")]
    [EnumDataType(typeof(FormatType), ErrorMessage = "Invalid format type.")]
    string FormatType,

    [Required(ErrorMessage = "Price is required.")]
    [Range(100, 10_000_000, ErrorMessage = "Price must be between 100 and 10000000.")]
    decimal Price,

    [Range(0, 100, ErrorMessage = "Weight must be between 0 and 100.")]
    decimal? Weight,

    [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
    int? Stock,

    [MaxLength(50, ErrorMessage = "Edition cannot be longer than 50 characters.")]
    string? Edition,

    bool IsAvailable = true
);

[UsedImplicitly]
public record UpdateBookFormatDto(
    [EnumDataType(typeof(FormatType), ErrorMessage = "Invalid format type.")]
    string? FormatType,

    [Range(100, 10_000_000, ErrorMessage = "Price must be between 100 and 10000000.")]
    decimal? Price,

    [Range(0, 100, ErrorMessage = "Weight must be between 0 and 100.")]
    decimal? Weight,

    [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
    int? Stock,

    [MaxLength(50, ErrorMessage = "Edition cannot be longer than 50 characters.")]
    string? Edition,

    bool? IsAvailable
);

[UsedImplicitly]
public record BookFormatDto(
    Guid Id,
    string? FormatType,
    decimal Price,
    decimal? Weight,
    bool IsAvailable,
    int? Stock,
    Guid BookId,
    string? KeyUrl,
    string? Edition
)
{
    public bool IsDigital => FormatType is "Ebook" or "Pdf";
}