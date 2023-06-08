using System;
using System.Collections.Generic;

namespace WP_Temp.Models;

public partial class User
{
    public int Id { get; set; }

    public string? NameSurname { get; set; }

    public string? EMail { get; set; }

    public string? Password { get; set; }

    public string? GoogleId { get; set; }

    public virtual ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();

    public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
}
