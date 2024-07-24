using System;
using System.Collections.Generic;

namespace StrategicviewBack.Models;

public partial class TbEmpresa
{
    public int IdEmpresa { get; set; }

    public string? Industria { get; set; }

    public string? NombreEmpresa { get; set; }

    public string? Pais { get; set; }

    public string? Departamento { get; set; }

    public string? Ciudad { get; set; }

    public int? CodigoPostal { get; set; }

    public string? DireccionEmpresa { get; set; }

    public string? TelefonoEmpresa { get; set; }

    public string? CorreoEmpresa { get; set; }

    public string? NombreGerente { get; set; }

    public string? ApellidoGerente { get; set; }

    public string? TelefonoGerente { get; set; }

    public string? CorreoGerente { get; set; }

    public string? NombreResponsable { get; set; }

    public string? ApellidoResponsable { get; set; }

    public string? TelefonoResponsable { get; set; }

    public string? CorreoResponsable { get; set; }

    public string? CargoResponsable { get; set; }

    public DateOnly? FechaCreacionEmpresa { get; set; }

    public virtual ICollection<TbEmpresasPermiso> TbEmpresasPermisos { get; set; } = new List<TbEmpresasPermiso>();

    public virtual ICollection<TbEmpresasUsuario> TbEmpresasUsuarios { get; set; } = new List<TbEmpresasUsuario>();

    public virtual ICollection<TbInformesJd> TbInformesJds { get; set; } = new List<TbInformesJd>();

    public virtual ICollection<TbProyecto> TbProyectos { get; set; } = new List<TbProyecto>();
}
