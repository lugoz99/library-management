
namespace LibraryManagement.Models;

public class Author:BaseEntity
{
    public required string FirstName { get; set; }
    public string? LastNames { get; set; }
    public string? Biography { get; set; }
    public string? Nationality { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public DateOnly? DateOfDeath { get; set; }
}