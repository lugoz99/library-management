using LibraryManagement.Models.Enums;

namespace LibraryManagement.Models;

public class BookFormat:BaseEntity
{
    public FormatType? FormatType { get; set; }
    public decimal Price { get; set; }
    public decimal? Weight { get;set; }
    public bool IsAvailable { get; set; } = true;
    public int? Stock {get;set;}
    public Guid BookId { get; set; }
    public Book Book { get; set; } = null!;
    public string? KeyUrl {get;set;}
    public string? Edition {get;set;}
    
    
    public bool IsDigital => FormatType is Enums.FormatType.Ebook or Enums.FormatType.Pdf;
}