using System;
using System.Collections.Generic;

namespace StrategicviewBack.Models;

public partial class TbRole
{
    public int IdRol { get; set; }

    public int? IdEmpresa { get; set; }

    public string? Rol { get; set; }

    public virtual ICollection<TbRolPermiso> TbRolPermisos { get; set; } = new List<TbRolPermiso>();

    public virtual ICollection<TbUsuario> TbUsuarios { get; set; } = new List<TbUsuario>();
}
