namespace StrategicviewBack.Models.DTO
{
    public class UsuarioDTO
    {
        public string? Email { get ; set; }

        public string? Nombrecompleto { get; set; }

        public string? Rol { get; set; }

        public string? payload { get; set; }

        public List<PermisoDTO>? permisos {get; set;}

    }
}