using LibraryManagement.Models.Enums;

namespace LibraryManagement.Models;

public class Promotion : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public PromotionDiscountType DiscountType { get; set; }
    public decimal DiscountValue { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public bool IsActive { get; set; } = true;

    public Guid BookId { get; set; }
    public Book Book { get; set; } = null!;
}
