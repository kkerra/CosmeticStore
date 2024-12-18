using System;
using System.Collections.Generic;

namespace CosmeticStoreLibrary.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public string OrderStatus { get; set; } = null!;

    public DateTime OrderDeliveryDate { get; set; }

    public int OrderPickupCode { get; set; }

    public DateTime OrderDate { get; set; }

    public int OrderPickupPointId { get; set; }

    public virtual PickupPoint OrderPickupPoint { get; set; } = null!;

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual User? User { get; set; }
}
