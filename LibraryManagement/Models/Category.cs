using System.ComponentModel.DataAnnotations;
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace LibraryManagement.Models
{
    public class Category : BaseEntity
    {
        [StringLength(100)]
        public required string Name { get; set; } = string.Empty;

        // An optional short description of the category (Max 500 characters)
        [StringLength(500)]
        public string? Description { get; set; }

        // Foreign Key: The ID of the parent category (Null if it is a main category)
        public Guid? ParentCategoryId { get; set; }

        // Navigation Property: Reference to the parent category object
        public Category? ParentCategory { get; set; }

        // Navigation Property: List of subcategories inside this category
        public ICollection<Category> SubCategories { get; set; } = [];
    }
}