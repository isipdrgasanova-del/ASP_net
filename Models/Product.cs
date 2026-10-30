using System;
using System.Collections.Generic;

namespace WebApplication13.Models;

public partial class Product
{
    public int IdProduct { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; }
    public int IdCategory { get; set; }

    // Вот эта строка была пропущена, из-за нее были красные ошибки:
    public string? ImageUrl { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public virtual Category IdCategoryNavigation { get; set; } = null!;
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}