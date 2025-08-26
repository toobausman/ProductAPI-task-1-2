using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }                              // int

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;         // string

        [Range(0, 1_000_000)]
        public decimal Price { get; set; }                       // decimal

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }                        // int
    }
}
