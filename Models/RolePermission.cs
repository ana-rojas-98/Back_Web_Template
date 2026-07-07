using System;
using System.Collections.Generic;

namespace StrategicviewBack.Models;

public partial class RolePermission
{
    public int RolePermissionId { get; set; }

    public int? PermissionId { get; set; }

    public int? RoleId { get; set; }

    public virtual Permission? PermissionNavigation { get; set; }

    public virtual Role? RoleNavigation { get; set; }
}
