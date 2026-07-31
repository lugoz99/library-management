using LibraryManagement.Common.Utils;
using Microsoft.VisualBasic.CompilerServices;

namespace LibraryManagement.Models;

public class Book:BaseEntity
{
    public string Title
    {
        get;
        set => field = Normalize.ToCapitalizeCase(value);
    } = null!;

    public required string Isbn { get; set; }
    public bool IsAvailable { get; set; } = true;
    public string? Description { get; set; }
    public string? CoverImageUrl { get; set; } 
    public string? CoverImageKey { get; set; }  

    // Llaves foráneas con lo que ya tienes
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }

    public Guid PublisherId { get; set; }
    public Publisher? Publisher { get; set; }
    
    public DateOnly PublicationDate { get; set; }
    
    public int PagesCount { get; set; }
    public required string Language { get; set; }
    public ICollection<BookFormat> BooksFormats { get; set; } = [];

    
}