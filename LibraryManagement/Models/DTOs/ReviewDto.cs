using JetBrains.Annotations;

namespace LibraryManagement.Models.DTOs;

[UsedImplicitly]
public record ReviewDto(
    Guid Id,
    string Title,
    string? Comment,
    int Rating,
    string? ReviewName,
    Guid BookId
    );
[UsedImplicitly]
public record CreateReviewDto(
    string Title,
    string? Comment,
    int Rating,
    string? ReviewName,
    Guid BookId
    );