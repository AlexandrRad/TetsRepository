using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAPI_React.Model;

public partial class User
{
    public int IdUsers { get; set; }

    public string UserName { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public bool StatusConfirmed { get; set; }

    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
   
    [JsonIgnore]
    public virtual RolesTable Role { get; set; } = null!;
    
    [JsonIgnore]
    public virtual ICollection<Shop> Shops { get; set; } = new List<Shop>();
}
