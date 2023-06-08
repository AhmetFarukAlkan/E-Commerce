using System;
using System.Collections.Generic;

namespace WP_Temp.Models;

public partial class OrderHistory
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? ProductId { get; set; }

    public int? SiparisAdedi { get; set; }

    public DateOnly? Tarih { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
