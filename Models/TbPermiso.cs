using System;
using System.Collections.Generic;

namespace StrategicviewBack.Models;

public partial class TbPermiso
{
    public int IdPermiso { get; set; }

    public string? Valor { get; set; }

    public virtual ICollection<TbRolPermiso> TbRolPermisos { get; set; } = new List<TbRolPermiso>();
}
