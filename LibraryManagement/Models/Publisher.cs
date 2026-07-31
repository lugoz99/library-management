
namespace LibraryManagement.Models;

public class Publisher:BaseEntity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string? Description { get; set; }
    public string? Website { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public bool IsActive {get;set;} = true;
    
    
    public ICollection<Book> Books { get; set; } = [];

}