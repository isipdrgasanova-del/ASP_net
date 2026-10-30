namespace WebApplication13.DTO
{
    public class OrderDto
    {
        public int IdOrder { get; set; }
        public int IdUser { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = null!;
        public DateTime OrderDate { get; set; } // В модели DateOnly, но для DTO можно DateTime
    }

    public class CreateOrderDto
    {
        public int IdUser { get; set; }
        public decimal TotalAmount { get; set; }
    }
}