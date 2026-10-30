using System.ComponentModel.DataAnnotations;

namespace WebApplication13.DTO
{
    public class UpdateProductDTO
    {
        [Required]
        [MaxLength(200)]
        public string ProductName { get; set; } = null!;

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue)]
        public int IdCategory { get; set; }

        [MaxLength(500)]
        public string? ImageUrl { get; set; }
    }
}