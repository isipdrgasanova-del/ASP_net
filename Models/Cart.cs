using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication13.Models;

public partial class Cart
{
    public int IdCart { get; set; }
    public int IdUser { get; set; }
    public DateOnly CreatedAt { get; set; }

    [InverseProperty("IdCartNavigation")]
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}