using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CosmeticStoreLibrary.Models;

public partial class PickupPoint
{
    public int PickupPointId { get; set; }

    public int? ZipCode { get; set; }

    public string? Address { get; set; }

    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
