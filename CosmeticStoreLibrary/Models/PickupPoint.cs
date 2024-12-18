using System;
using System.Collections.Generic;

namespace CosmeticStoreLibrary.Models;

public partial class PickupPoint
{
    public int PickupPointId { get; set; }

    public int? ZipCode { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
