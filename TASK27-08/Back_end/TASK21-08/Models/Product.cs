using System;
using System.Collections.Generic;

namespace TASK21_08.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? CategoryId { get; set; }

    public string? ProductImage { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
