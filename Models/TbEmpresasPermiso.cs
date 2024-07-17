using System;
using System.Collections.Generic;

namespace StrategicviewBack.Models;

public partial class TbEmpresasPermiso
{
    public int IdEmpresapermiso { get; set; }

    public int? IdEmpresa { get; set; }

    public int? IdPermiso { get; set; }

    public virtual TbEmpresa? IdEmpresaNavigation { get; set; }

    public virtual TbPermiso? IdPermisoNavigation { get; set; }
}
