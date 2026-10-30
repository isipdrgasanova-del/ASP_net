namespace WebApplication13.DTO
{
    public class ProductDTO
    {
        public int IdProduct { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public int IdCategory { get; set; }
        public string? ImageUrl { get; set; }
    }
}