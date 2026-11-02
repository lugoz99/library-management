namespace LibraryManagement.Models;

public class WishListItem
{
    public Guid WishListId { get; set; }
    public WishList WishList { get; set; } = null!;

    public Guid BookId { get; set; }
    public Book Book { get; set; } = null!;
}
