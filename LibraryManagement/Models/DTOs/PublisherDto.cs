using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace LibraryManagement.Models.DTOs;

[UsedImplicitly]
public record CreatePublisherDto(
    [Required(ErrorMessage = "The name is required.")]
    string Name,
    [Required(ErrorMessage = "The email is required.")]
    [EmailAddress(ErrorMessage = "The email address is not valid.")]
    string Email,
    string? Description,
    [Url(ErrorMessage = "The website URL is not valid.")]
    string? Website
);

[UsedImplicitly]
public record PublisherDto(
    Guid Id,
    string Name,
    string Email,
    string? Description,
    string? Website,
    bool IsActive
);

[UsedImplicitly]
public record UpdatePublisherDto
(
    string Name,
    [EmailAddress(ErrorMessage = "The email address is not] valid.")]
    string? Email,
    string? Description,
    string? Website,
    [Url(ErrorMessage = "The website URL is not valid.")]
    bool? IsActive = true
);