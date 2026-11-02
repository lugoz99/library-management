namespace LibraryManagement.Models;

public class BookAuthors:BaseEntity
{
    public Guid BookId { get; set; }
    public Book Book { get; set; } = null!;
    public Guid AuthorId { get; set; }
    public Author Author { get; set; } = null!;
    public string Role { get; set; } = string.Empty;
}