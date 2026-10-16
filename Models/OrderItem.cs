using System;
using System.Collections.Generic;

namespace WebApplication12.Models;

public partial class OrderItem
{
    public int IdOrderItem { get; set; }

    public int IdOrder { get; set; }

    public int IdProduct { get; set; }

    public int Quantity { get; set; }

    public decimal PriceAtOrder { get; set; }

    public virtual Order IdOrderNavigation { get; set; } = null!;

    public virtual Product IdProductNavigation { get; set; } = null!;
}
