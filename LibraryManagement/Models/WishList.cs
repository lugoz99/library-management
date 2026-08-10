namespace LibraryManagement.Models;

public class WishList:BaseEntity
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Cada wishlist pertenece a un usuario
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public ICollection<Book> Books { get; set; } = [];
}