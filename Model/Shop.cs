using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAPI_React.Model;

public partial class Shop
{
    public int IdShop { get; set; }

    public int Count { get; set; }

    public decimal Price { get; set; }

    public int IdProduct { get; set; }

    public int IdUser { get; set; }

    [JsonIgnore]
    public virtual Product IdProductNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual User IdUserNavigation { get; set; } = null!;
}
