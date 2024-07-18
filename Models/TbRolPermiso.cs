using System;
using System.Collections.Generic;

namespace StrategicviewBack.Models;

public partial class TbRolPermiso
{
    public int IdRolpermiso { get; set; }

    public int? IdPermiso { get; set; }

    public int? IdUsuario { get; set; }

    public virtual TbPermiso? IdPermisoNavigation { get; set; }

    public virtual TbUsuario? IdUsuarioNavigation { get; set; }
}
