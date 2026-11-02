using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace LibraryManagement.Models.DTOs;

public record CreateCategoryDto(
    [Required(ErrorMessage = "The name is required.")] 
    [StringLength(100, ErrorMessage = "The name cannot exceed 100 characters.")]
    string Name,

    [StringLength(500, ErrorMessage = "The description cannot exceed 500 characters.")]
    string? Description,

    Guid? ParentCategoryId = null
);

public record UpdateCategoryDto(
    [Required(ErrorMessage = "The name is required.")]
    [StringLength(100, ErrorMessage = "The name cannot exceed 100 characters.")]
    string Name,

    [StringLength(500, ErrorMessage = "The description cannot exceed 500 characters.")]
    string? Description,

    Guid? ParentCategoryId = null
);

[UsedImplicitly]
public record CategoryDto(
    Guid Id, 
    string Name,
    string? Description,
    Guid? ParentCategoryId
);

[UsedImplicitly]
public record CategoryDetailDto(
    Guid Id, 
    string Name,
    string? Description,
    Guid? ParentCategoryId,
    IEnumerable<CategoryDto> SubCategories
);