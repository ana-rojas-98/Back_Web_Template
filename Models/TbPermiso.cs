using System;
using System.Collections.Generic;

namespace StrategicviewBack.Models;

public partial class TbPermiso
{
    public int IdPermiso { get; set; }

    public string? NombrePermiso { get; set; }

    public string? UrlAplicacion { get; set; }

    public string? Icon { get; set; }

    public bool? Padre { get; set; }

    public int? IdPermisopadre { get; set; }

    public virtual ICollection<TbRolPermiso> TbRolPermisos { get; set; } = new List<TbRolPermiso>();
}
