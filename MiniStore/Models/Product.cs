using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniStore.Models
{
    public class Product
    {
        public int Id { get; set; }                              
        [Required, StringLength(120)] public string Name { get; set; } = "";
        [Column(TypeName = "decimal(18,2)")] public decimal Price { get; set; } 
        public int Stock { get; set; }                            
        public int CategoryId { get; set; }                       

        
        public Category? Category { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
