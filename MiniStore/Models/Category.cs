using System.ComponentModel.DataAnnotations;

namespace MiniStore.Models
{
    public class Category
    {
        public int Id { get; set; }                              // 1
        [Required, StringLength(80)] public string Name { get; set; } = ""; // 2
        [StringLength(200)] public string? Description { get; set; }        // 3
        public bool IsActive { get; set; } = true;               // 4
        [DataType(DataType.Date)] public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // 5

        // Navegación (no cuenta como atributo de tabla)
        public ICollection<Product>? Products { get; set; }
    }
}
