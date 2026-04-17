using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAPI_React.Model;

public partial class RolesTable
{
    public int IdRoles { get; set; }

    public string RoleName { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
