using System;
using System.Collections.Generic;

namespace StrategicviewBack.Models;

public partial class TbProyecto
{
    public int IdProyecto { get; set; }

    public int? IdEmpresa { get; set; }

    public string? VerticalNegocio { get; set; }

    public string? ProyectoNombre { get; set; }

    public string? Objetivo { get; set; }

    public string? Cliente { get; set; }

    public string? LiderProyecto { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFinal { get; set; }

    public virtual TbEmpresa? IdEmpresaNavigation { get; set; }
}
