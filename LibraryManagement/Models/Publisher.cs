
namespace LibraryManagement.Models;

public class Publisher:BaseEntity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string? Description { get; set; }
    public string? Website { get; set; }
    public bool IsActive {get;set;} = true;

}