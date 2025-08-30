using System.ComponentModel.DataAnnotations;

namespace MiniStore.Models
{
    public class Customer
    {

        public int Id { get; set; }                                 // 1
        [Required, StringLength(120)] public string FullName { get; set; } = ""; // 2
        [Required, EmailAddress, StringLength(120)] public string Email { get; set; } = ""; // 3
        [Phone, StringLength(20)] public string? Phone { get; set; } // 4
        public bool IsActive { get; set; } = true;                   // 5

       
        public ICollection<Order>? Orders { get; set; }
    }
}
