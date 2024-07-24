using System;
using System.Collections.Generic;

namespace StrategicviewBack.Models;

public partial class TbRolPermiso
{
    public int IdRolpermiso { get; set; }

    public int? IdPermiso { get; set; }

    public int? IdRol { get; set; }

    public virtual TbPermiso? IdPermisoNavigation { get; set; }

    public virtual TbRole? IdRolNavigation { get; set; }
}
