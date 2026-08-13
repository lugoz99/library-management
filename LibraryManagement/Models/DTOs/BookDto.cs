using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace LibraryManagement.Models.DTOs;

// ============================================================
// BOOK - CREATE
// ============================================================

[UsedImplicitly]
public record CreateBookDto(
    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
    string Title,

    [Required(ErrorMessage = "ISBN is required.")]
    [MaxLength(13, ErrorMessage = "ISBN cannot be longer than 13 characters.")]
    string Isbn,

    [MaxLength(2000, ErrorMessage = "Description cannot be longer than 2000 characters.")]
    string? Description,

    [Required(ErrorMessage = "Category is required.")]
    Guid CategoryId,

    [Required(ErrorMessage = "Publisher is required.")]
    Guid PublisherId,

    [Required(ErrorMessage = "Publication date is required.")]
    DateOnly PublicationDate,

    [Range(1, 10000, ErrorMessage = "Page count must be between 1 and 10000.")]
    int PagesCount,

    [Required(ErrorMessage = "Language is required.")]
    [MaxLength(10, ErrorMessage = "Language cannot be longer than 10 characters.")]
    string Language,

    bool IsAvailable = true
);

// ============================================================
// BOOK - UPDATE
// ============================================================

[UsedImplicitly]
public record UpdateBookDto(
    [MaxLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
    string? Title = null,

    [MaxLength(13, ErrorMessage = "ISBN cannot be longer than 13 characters.")]
    string? Isbn = null,

    [MaxLength(2000, ErrorMessage = "Description cannot be longer than 2000 characters.")]
    string? Description = null,

    Guid? CategoryId = null,

    Guid? PublisherId = null,

    DateOnly? PublicationDate = null,

    [Range(1, 10000, ErrorMessage = "Page count must be between 1 and 10000.")]
    int? PagesCount = null,

    [MaxLength(10, ErrorMessage = "Language cannot be longer than 10 characters.")]
    string? Language = null,

    bool? IsAvailable = null
);

// ============================================================
// BOOK - LIST RESPONSE (light)
// ============================================================

[UsedImplicitly]
public record BookDto(
    Guid Id,
    string Title,
    string Isbn,
    bool IsAvailable,
    string? Description,
    string? CoverImageUrl,
    Guid CategoryId,
    Guid PublisherId,
    DateOnly? PublicationDate,
    int PagesCount,
    string Language
);

// ============================================================
// BOOK + AUTHORS - INPUT
// ============================================================

[UsedImplicitly]
public record BookAuthorItemDto(
    [Required(ErrorMessage = "Author is required.")]
    Guid AuthorId,

    [Required(ErrorMessage = "Role is required.")]
    [MaxLength(50, ErrorMessage = "Role cannot be longer than 50 characters.")]
    string Role
);

[UsedImplicitly]
public record AddBookAuthorDto(
    [Required(ErrorMessage = "Author is required.")]
    Guid AuthorId,

    [Required(ErrorMessage = "Role is required.")]
    [MaxLength(50, ErrorMessage = "Role cannot be longer than 50 characters.")]
    string Role
);

[UsedImplicitly]
public record CreateBookWithAuthorsDto(
    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
    string Title,

    [Required(ErrorMessage = "ISBN is required.")]
    [MaxLength(13, ErrorMessage = "ISBN cannot be longer than 13 characters.")]
    string Isbn,

    [MaxLength(2000, ErrorMessage = "Description cannot be longer than 2000 characters.")]
    string? Description,

    [Required(ErrorMessage = "Category is required.")]
    Guid CategoryId,

    [Required(ErrorMessage = "Publisher is required.")]
    Guid PublisherId,

    [Required(ErrorMessage = "Publication date is required.")]
    DateOnly PublicationDate,

    [Range(1, 10000, ErrorMessage = "Page count must be between 1 and 10000.")]
    int PagesCount,

    [Required(ErrorMessage = "Language is required.")]
    [MaxLength(10, ErrorMessage = "Language cannot be longer than 10 characters.")]
    string Language,

    bool IsAvailable = true,

    IReadOnlyList<BookAuthorItemDto>? Authors = null
);

// ============================================================
// BOOK + AUTHORS - CREATE RESPONSE (ids only)
// ============================================================

[UsedImplicitly]
public record CreateBookWithAuthorsResponseDto(
    Guid Id,
    string Title,
    string Isbn,
    bool IsAvailable,
    string? Description,
    string? CoverImageUrl,
    Guid CategoryId,
    Guid PublisherId,
    DateOnly? PublicationDate,
    int PagesCount,
    string Language,
    IReadOnlyList<BookAuthorItemDto> Authors
);

// ============================================================
// BOOK - DETAIL RESPONSE (GetById)
// ============================================================

[UsedImplicitly]
public record CategorySummaryDto(
    Guid Id,
    string Name
);

[UsedImplicitly]
public record PublisherSummaryDto(
    Guid Id,
    string Name
);

[UsedImplicitly]
public record BookAuthorResponseDto(
    Guid AuthorId,
    string FirstName,
    string? LastNames,
    string Role
);

[UsedImplicitly]
public record BookResponseDto(
    Guid Id,
    string Title,
    string Isbn,
    bool IsAvailable,
    string? Description,
    string? CoverImageUrl,
    DateOnly? PublicationDate,
    int PagesCount,
    string Language,
    CategorySummaryDto? Category,
    PublisherSummaryDto? Publisher,
    IReadOnlyList<BookAuthorResponseDto> Authors
);