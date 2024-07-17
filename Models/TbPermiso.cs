using System;
using System.Collections.Generic;

namespace StrategicviewBack.Models;

public partial class TbPermiso
{
    public int IdPermiso { get; set; }

    public string? Valor { get; set; }

    public virtual ICollection<TbEmpresasPermiso> TbEmpresasPermisos { get; set; } = new List<TbEmpresasPermiso>();
}
