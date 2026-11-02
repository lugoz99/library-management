using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace LibraryManagement.Models.DTOs;

// Response DTO: Sin validaciones, solo refleja la entidad
[UsedImplicitly]
public record AuthorDto(
    Guid Id,
    string FirstName,
    string? LastNames,
    string? Biography,
    string? Nationality,
    DateOnly? DateOfBirth,
    DateOnly? DateOfDeath);

// Create DTO: FirstName ES requerido (string + [Required]). Los demás son opcionales (string?).
public record CreateAuthorDto(
    [Required(ErrorMessage = "The first name is required.")]
    [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters.")]
    string FirstName, 

    [StringLength(100, ErrorMessage = "Last names cannot exceed 100 characters.")]
    string? LastNames,

    [StringLength(2000, ErrorMessage = "Biography cannot exceed 2000 characters.")]
    string? Biography,

    [StringLength(50, ErrorMessage = "Nationality cannot exceed 50 characters.")]
    string? Nationality,

    DateOnly? DateOfBirth,
    DateOnly? DateOfDeath);

// Update DTO: Todo es opcional (string? = null) para permitir actualizaciones parciales.
public record UpdateAuthorDto(
    [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters.")]
    string? FirstName = null,

    [StringLength(100, ErrorMessage = "Last names cannot exceed 100 characters.")]
    string? LastNames = null,

    [StringLength(2000, ErrorMessage = "Biography cannot exceed 2000 characters.")]
    string? Biography = null,

    [StringLength(50, ErrorMessage = "Nationality cannot exceed 50 characters.")]
    string? Nationality = null,

    DateOnly? DateOfBirth = null,
    DateOnly? DateOfDeath = null);