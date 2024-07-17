using System;
using System.Collections.Generic;

namespace StrategicviewBack.Models;

public partial class TbUsuario
{
    public int IdUsuario { get; set; }

    public int? IdRol { get; set; }

    public int? IdProyecto { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? TipoIdentificacion { get; set; }

    public int? Identificacion { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? CorreoElectronico { get; set; }

    public string? Telefono { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public DateOnly? FechaIngresoEmpresa { get; set; }

    public virtual TbRole IdUsuarioNavigation { get; set; } = null!;

    public virtual TbRolEmpresaPermiso? TbRolEmpresaPermiso { get; set; }
}
