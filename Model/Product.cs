using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAPI_React.Model;

public partial class Product
{
    public int IdProduct { get; set; }

    public string ProductName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Count { get; set; }

    public string Category { get; set; } = null!;

    public string Image { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [JsonIgnore]
    public virtual ICollection<Shop> Shops { get; set; } = new List<Shop>();
}
