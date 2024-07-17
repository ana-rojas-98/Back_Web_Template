using System;
using System.Collections.Generic;

namespace StrategicviewBack.Models;

public partial class TbRolEmpresaPermiso
{
    public int IdRolempresapermiso { get; set; }

    public int? IdEmpresapermiso { get; set; }

    public string? IdUsuario { get; set; }

    public virtual TbUsuario IdRolempresapermisoNavigation { get; set; } = null!;
}
