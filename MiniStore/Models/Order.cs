using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniStore.Models
{
    public class Order
    {
        public int Id { get; set; }                               // 1
        public DateTime OrderDate { get; set; } = DateTime.UtcNow; // 2
        [Column(TypeName = "decimal(18,2)")] public decimal Total { get; set; } // 3
        public int CustomerId { get; set; }                       // 4
        [Required, StringLength(20)] public string Status { get; set; } = "Pending"; // 5

        
        public Customer? Customer { get; set; }
        public ICollection<OrderItem>? Items { get; set; }
    }
}
