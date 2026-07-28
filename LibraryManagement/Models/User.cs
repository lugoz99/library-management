namespace LibraryManagement.Models;

public class User:BaseEntity
{ 
   
   public required string FirstName { get; set; }
   public string? LastNames { get; set; }
   public required string UserName { get; set; }
   public required string Email { get; set; }
   public required string PasswordHash { get; set; }
   public bool IsActive { get; set; } = true;
}