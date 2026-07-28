namespace LibraryManagement.Models;

public class BookAuthor:BaseEntity
{
    public Guid BookId { get; set; }
    public Book Book { get; set; } = null!;
    public Guid AuthorId { get; set; }
    public Author Author { get; set; } = null!;
    public string? Role { get; set; } // e.g., "Author", "Editor", etc.
    
}