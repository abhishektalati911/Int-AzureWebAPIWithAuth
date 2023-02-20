using System.ComponentModel.DataAnnotations;

namespace AzureWebAPIWithAuth.Models
{
    public class Inventory
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
