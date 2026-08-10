namespace LibraryManagement.Models.DTOs;

public record ReviewDto(
    Guid Id,
    string Title,
    string? Comment,
    int Rating,
    string? ReviewName,
    Guid BookId
    );
public record CreateReviewDto(
    string Title,
    string? Comment,
    int Rating,
    string? ReviewName,
    Guid BookId
    );