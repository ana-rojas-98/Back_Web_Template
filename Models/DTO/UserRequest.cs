namespace StrategicviewBack.Models.DTO // Cambia 'YourNamespace' por el namespace adecuado
{
    public class UserRequest
    {
        public string? IdUsuario { get; set; }
        public int IdEmpresa { get; set; }
        public string? EmpresasUsuarioJson { get; set; }
        public string? Correo { get; set; }
        public string? Nombre { get; set; }
        public string? Rol { get; set; }
        public string? permisosJson { get; set; }
    }
}