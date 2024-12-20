using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CosmeticStoreLibrary.Models;

public partial class OrderProduct
{
    public int OrderId { get; set; }

    public string ProductArticleNumber { get; set; } = null!;

    public int ProductAmount { get; set; }

    [JsonIgnore]
    public virtual Order Order { get; set; } = null!;

    [JsonIgnore]
    public virtual Product ProductArticleNumberNavigation { get; set; } = null!;
}
