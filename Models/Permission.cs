using System;
using System.Collections.Generic;

namespace StrategicviewBack.Models;

public partial class Permission
{
    public int PermissionId { get; set; }

    public string? PermissionName { get; set; }

    public string? ApplicationUrl { get; set; }

    public string? Icon { get; set; }

    public bool? IsParent { get; set; }

    public int? ParentPermissionId { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
