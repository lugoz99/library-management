namespace LibraryManagement.Models;


public class User : BaseEntity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    
    public ICollection<WishList> WishLists { get; set; } = [];
    public ICollection<Review> Reviews { get; set; } = [];
}