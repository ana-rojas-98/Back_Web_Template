using System;
using System.Collections.Generic;

namespace StrategicviewBack.Models;

public partial class TbInformesJd
{
    public int IdInforme { get; set; }

    public int? IdEmpresa { get; set; }

    public string? TituloInforme { get; set; }

    public string? Responsable { get; set; }

    public string? Etapa { get; set; }

    public string? Estado { get; set; }

    public int? PorcentajeCumplimiento { get; set; }

    public DateOnly? FechaJunta { get; set; }

    public DateOnly? FechaEntrega { get; set; }

    public virtual TbEmpresa? IdEmpresaNavigation { get; set; }
}
