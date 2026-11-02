using LibraryManagement.Models.Enums;

namespace LibraryManagement.Models;

public class Order : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public decimal TotalAmount { get; set; }
    public string Currency { get; set; } = "usd";
    public string? StripeCheckoutSessionId { get; set; }
    public string? StripePaymentIntentId { get; set; }
    public DateTime? PaidAt { get; set; }
    public ICollection<OrderItem> Items { get; set; } = [];
}
