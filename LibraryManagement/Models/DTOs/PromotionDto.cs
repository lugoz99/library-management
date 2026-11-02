using System.ComponentModel.DataAnnotations;
using LibraryManagement.Models.Enums;

namespace LibraryManagement.Models.DTOs;

public record PromotionDto(
    Guid Id,
    Guid BookId,
    string Name,
    string? Description,
    PromotionDiscountType DiscountType,
    decimal DiscountValue,
    DateTime StartsAt,
    DateTime EndsAt,
    bool IsActive,
    bool IsCurrentlyActive
);

public record CreatePromotionDto(
    [property: Required, StringLength(120, MinimumLength = 2)]
    string Name,
    [property: StringLength(500)]
    string? Description,
    PromotionDiscountType DiscountType,
    [property: Range(typeof(decimal), "0.01", "100000000")]
    decimal DiscountValue,
    DateTime StartsAt,
    DateTime EndsAt
);
