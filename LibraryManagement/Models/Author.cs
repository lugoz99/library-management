
namespace LibraryManagement.Models;

public class Author:BaseEntity
{
    public required string FirstName { get; set; }
    public string? LastNames { get; set; }
    public string? Biography { get; set; }
    public string? Nationality { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime? DateOfDeath { get; set; }
}