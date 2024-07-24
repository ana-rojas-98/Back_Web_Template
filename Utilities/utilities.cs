using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using StrategicviewBack.Models.DTO; 

namespace StrategicviewBack.Utilities
{
    public static class UserUtility
    {
        public static UserRequest GetUserRequestFromClaims(HttpContext httpContext)
        {
            var idUsuario = httpContext.User.FindFirst("id_usuario")?.Value;
            var idEmpresaClaim = httpContext.User.FindFirst("id_empresa")?.Value;
            var idEmpresa = string.IsNullOrEmpty(idEmpresaClaim) ? 0 : Convert.ToInt16(idEmpresaClaim);
            var empresasUsuarioJson = httpContext.User.FindFirst("empresas_usuario")?.Value;
            var correo = httpContext.User.FindFirst(ClaimTypes.Email)?.Value ?? httpContext.User.FindFirst("correo")?.Value;
            var nombre = httpContext.User.FindFirst("nombrecompleto")?.Value;
            var rol = httpContext.User.FindFirst("rolusuario")?.Value;
            var permisosJson = httpContext.User.FindFirst("permisosJson")?.Value;

            return new UserRequest
            {
                IdUsuario = idUsuario,
                IdEmpresa = idEmpresa,
                EmpresasUsuarioJson = empresasUsuarioJson,
                Correo = correo,
                Nombre = nombre,
                Rol = rol,
                permisosJson = permisosJson
            };
        }
    }
}
