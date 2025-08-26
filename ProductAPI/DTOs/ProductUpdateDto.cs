using System.ComponentModel.DataAnnotations;

namespace ProductAPI.DTOs
{
    public class ProductUpdateDto
    {
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(0, 1_000_000)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
