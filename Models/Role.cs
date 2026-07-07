using System;
using System.Collections.Generic;

namespace StrategicviewBack.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public int? CompanyId { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
