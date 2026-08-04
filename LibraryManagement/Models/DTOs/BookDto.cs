using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace LibraryManagement.Models.DTOs;

[UsedImplicitly]
public record CreateBookDto(
    [property: Required(ErrorMessage = "Title is required.")]
    [property: MaxLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
    string Title,

    [property: Required(ErrorMessage = "ISBN is required.")]
    [property: MaxLength(13, ErrorMessage = "ISBN cannot be longer than 13 characters.")]
    string Isbn,

    [property: MaxLength(2000, ErrorMessage = "Description cannot be longer than 2000 characters.")]
    string? Description,

    [property: Required(ErrorMessage = "Category is required.")]
    Guid CategoryId,

    [property: Required(ErrorMessage = "Publisher is required.")]
    Guid PublisherId,

    [property: Required(ErrorMessage = "Publication date is required.")]
    DateOnly PublicationDate,

    [property: Range(1, 10000, ErrorMessage = "Page count must be between 1 and 10000.")]
    int PagesCount,

    [property: Required(ErrorMessage = "Language is required.")]
    [property: MaxLength(10, ErrorMessage = "Language cannot be longer than 10 characters.")]
    string Language,

    bool IsAvailable = true

);

[UsedImplicitly]
public record UpdateBookDto(
    [property: MaxLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
    string? Title = null,

    [property: MaxLength(13, ErrorMessage = "ISBN cannot be longer than 13 characters.")]
    string? Isbn = null,

    [property: MaxLength(2000, ErrorMessage = "Description cannot be longer than 2000 characters.")]
    string? Description = null,

    Guid? CategoryId = null,

    Guid? PublisherId = null,

    DateOnly? PublicationDate = null,

    [property: Range(1, 10000, ErrorMessage = "Page count must be between 1 and 10000.")]
    int? PagesCount = null,

    [property: MaxLength(10, ErrorMessage = "Language cannot be longer than 10 characters.")]
    string? Language = null,

    bool? IsAvailable = null
);

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

[UsedImplicitly]
public record BookAuthorItemDto(
    [property: Required(ErrorMessage = "Author is required.")]
    Guid AuthorId,

    [property: Required(ErrorMessage = "Role is required.")]
    [property: MaxLength(50, ErrorMessage = "Role cannot be longer than 50 characters.")]
    string Role
);

[UsedImplicitly]
public record AddBookAuthorDto(
    [property: Required(ErrorMessage = "Author is required.")]
    Guid AuthorId,

    [property: Required(ErrorMessage = "Role is required.")]
    [property: MaxLength(50, ErrorMessage = "Role cannot be longer than 50 characters.")]
    string Role
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
    Guid CategoryId,
    Guid PublisherId,
    DateOnly? PublicationDate,
    int PagesCount,
    string Language,
    IReadOnlyList<BookAuthorResponseDto> Authors
);
//##### HANDLE BOOK - AUTHORS #####################
[UsedImplicitly]
public record CreateBookAuthorskDto(
    [property: Required(ErrorMessage = "Title is required.")]
    [property: MaxLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
    string Title,

    [property: Required(ErrorMessage = "ISBN is required.")]
    [property: MaxLength(13, ErrorMessage = "ISBN cannot be longer than 13 characters.")]
    string Isbn,

    [property: MaxLength(2000, ErrorMessage = "Description cannot be longer than 2000 characters.")]
    string? Description,

    [property: Required(ErrorMessage = "Category is required.")]
    Guid CategoryId,

    [property: Required(ErrorMessage = "Publisher is required.")]
    Guid PublisherId,

    [property: Required(ErrorMessage = "Publication date is required.")]
    DateOnly PublicationDate,

    [property: Range(1, 10000, ErrorMessage = "Page count must be between 1 and 10000.")]
    int PagesCount,

    [property: Required(ErrorMessage = "Language is required.")]
    [property: MaxLength(10, ErrorMessage = "Language cannot be longer than 10 characters.")]
    string Language,

    bool IsAvailable = true,

    List<BookAuthorItemDto>? Authors = null
);