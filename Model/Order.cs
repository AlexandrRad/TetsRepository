using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAPI_React.Model;

public partial class Order
{
    public int IdOrders { get; set; }

    public int ProductId { get; set; }

    public int UserId { get; set; }

    [JsonIgnore]
    public virtual Product Product { get; set; } = null!;

    [JsonIgnore]
    public virtual User User { get; set; } = null!;
}
