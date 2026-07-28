namespace LibraryManagement.Models;

public class Review:BaseEntity
{
    
    public string? Comment { get; set; }
    public required int Rating { get; set; }
}