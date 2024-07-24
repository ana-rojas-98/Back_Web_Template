
namespace StrategicviewBack.Models.DTO
{
 public class PermisoDTO
 {
    public int IdPermiso { get; set; }
    public string? NombrePermiso { get; set; }
    public string? Url { get; set; }
    public bool? Padre { get; set; }
    public int? IdPermisoPadre { get; set; }
    public string? Icon { get; set; }
    public List<PermisoDTO> PermisosHijos { get; set; } = new List<PermisoDTO>();
 }

}