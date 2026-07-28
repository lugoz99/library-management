namespace LibraryManagement.Models;

public class WishList:BaseEntity
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
}