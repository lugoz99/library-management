using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace LibraryManagement.Models.DTOs
{
    public record CreateCategoryDto(
        [Required(ErrorMessage = "The name is required.")] 
        [StringLength(100, ErrorMessage = "The name cannot exceed 100 characters.")]
        string Name,
        string? Description
    );

    public record UpdateCategoryDto(
        [Required(ErrorMessage = "The name is required.")]
        [StringLength(100, ErrorMessage = "The name cannot exceed 100 characters.")]
        string Name,
        string? Description
    );

    [UsedImplicitly]
    public record CategoryResponseDto(
        Guid Id, // Match with BaseEntity Guid
        string Name,
        string? Description,
        // Using a clean collection for related items. It can be empty, never null.
        IEnumerable<CategoryResponseDto> SubCategories
    );
}
