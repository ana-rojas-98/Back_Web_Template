using System;
using System.Collections.Generic;

namespace StrategicviewBack.Models;

public partial class TbEmpresasUsuario
{
    public int IdEmpresasusuarios { get; set; }

    public int? IdEmpresa { get; set; }

    public int? IdUsuario { get; set; }

    public virtual TbEmpresa? IdEmpresaNavigation { get; set; }

    public virtual TbUsuario? IdUsuarioNavigation { get; set; }
}
