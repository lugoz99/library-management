using System.ComponentModel.DataAnnotations;
using LibraryManagement.Common.Utils;

namespace LibraryManagement.Models;

public class Review:BaseEntity
{
    [MaxLength(50)]
    public string Title
    {
        get;
        set => field = Normalize.ToCapitalizeCase(value);
    } = null!;
    
    public string? Comment { get; set; }
    public required int Rating { get; set; }
    
    // Cada review pertenece a un usuario
    public Guid UserId { get; set; }
    public User? User { get; set; }
    
    public Guid BookId { get; set; }
    public Book Book { get; set; } = null!;
}