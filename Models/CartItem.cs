using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication13.Models;

public partial class CartItem
{
    public int IdCartItem { get; set; }
    public int IdCart { get; set; }
    public int IdProduct { get; set; }
    public int Quantity { get; set; }

    [ForeignKey("IdCart")]
    public virtual Cart IdCartNavigation { get; set; } = null!;

    [ForeignKey("IdProduct")]
    public virtual Product IdProductNavigation { get; set; } = null!;
}