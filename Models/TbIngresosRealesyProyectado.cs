using System;
using System.Collections.Generic;

namespace StrategicviewBack.Models;

public partial class TbIngresosRealesyProyectado
{
    public int IdIngreso { get; set; }

    public int? IdEmpresa { get; set; }

    public DateOnly? FechaIngreso { get; set; }

    public string? VerticalNegocio { get; set; }

    public string? CategoriaIngreso { get; set; }

    public string? ConceptoIngreso { get; set; }

    public int? Cantidad { get; set; }

    public int? ValorIngresoReal { get; set; }

    public int? ValorIngresoProyectado { get; set; }
}
