using System;
using System.Collections.Generic;

namespace WebApplication12.Models;

public partial class Position
{
    public int IdPosition { get; set; }

    public string PositionName { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
