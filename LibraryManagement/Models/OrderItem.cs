namespace LibraryManagement.Models;

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public Guid BookId { get; set; }
    public Book Book { get; set; } = null!;
    public Guid BookFormatId { get; set; }
    public BookFormat BookFormat { get; set; } = null!;
    public string BookTitle { get; set; } = null!;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; } = 1;
}
