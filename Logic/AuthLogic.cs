using StrategicviewBack.Models;
using StrategicviewBack.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using static IterumBackend.Token;
using System.Security.Claims;
using Newtonsoft.Json;

namespace StrategicviewBack.Logic
{

    public class AuthLogic{


        HelperToken helper;

        private readonly Way2godbContext _context;

        public AuthLogic(Way2godbContext context, IConfiguration configuration)
        {
            _context = context;
            this.helper = new HelperToken(configuration);
        }


        public async Task<ResponseModel<UsuarioDTO>> Authenticate(LoginDTO login)
        {

            try
            {

             var usuario = await _context.TbUsuarios.Where(usuario =>
                usuario.CorreoElectronico == login.Email &&
                usuario.Password == login.Password
             ).Include(x => x.IdRolNavigation).FirstOrDefaultAsync();

        
            if( usuario == null)
            {
              return new ResponseModel<UsuarioDTO>(true,true, "Credenciales incorrectas");
            }
            else
            {

                if(usuario.IdRolNavigation == null)
                {
                    return new ResponseModel<UsuarioDTO>(true,true, "El usuario no tiene un rol asignado");
                }

             var EmpresasUsuario = await _context.TbEmpresasUsuarios.
                                 Where(x => x.IdUsuario == usuario.IdUsuario).Select(u => new EmpresaUsuarioDTO
                                 {   
                                  IdUsuario = u.IdUsuario, 
                                  IdEmpresa = u.IdEmpresa
                                 })
            
                                 .ToListAsync(); 

             if(EmpresasUsuario == null)
              {
                    return new ResponseModel<UsuarioDTO>(true,true, "El usuario no empresa asignada");
              }

              /// obtener permisos rol 
              
              //var permisos = await _context.TbRolPermisos.Where(n => n.IdRol == usuario.IdRolNavigation.IdRol).ToListAsync();

              // Obtener los permisos hijos
              var idRol = usuario.IdRolNavigation.IdRol; 
              var permisosHijos = await _context.TbRolPermisos
              .Where(rp => rp.IdRol == idRol).Join(_context.TbPermisos,rp => rp.IdPermiso,p => p.IdPermiso,(rp, p) => 
              new PermisoDTO
               {
                  IdPermiso = p.IdPermiso,
                  NombrePermiso = p.NombrePermiso,
                  Url = p.UrlAplicacion,
                  Padre = p.Padre,
                  IdPermisoPadre = p.IdPermisopadre,
                  Icon = p.Icon
               }).Where(p => p.Padre == false)
               .ToListAsync();

               // Obtener los permisos padres
               // Obtener los permisos padres directamente desde los permisos hijos
               var permisosPadres = await _context.TbPermisos
               .Where(p => permisosHijos.Select(h => h.IdPermisoPadre).Contains(p.IdPermiso))
               .Select(p => new PermisoDTO
               {
                IdPermiso = p.IdPermiso,
                NombrePermiso = p.NombrePermiso,
                Url = p.UrlAplicacion,
                Padre = p.Padre,
                IdPermisoPadre = p.IdPermisopadre,
                Icon = p.Icon
               }).ToListAsync();


               foreach (var permisoPadre in permisosPadres)
               {
                permisoPadre.PermisosHijos = permisosHijos
                .Where(h => h.IdPermisoPadre == permisoPadre.IdPermiso)
                .ToList();
               }


             string idempresa = null; 

             if(EmpresasUsuario.Count()==1)
             {
                idempresa =  EmpresasUsuario.FirstOrDefault()?.IdEmpresa.ToString() ?? string.Empty;
             }
            
            
            var empresasUsuarioJson = JsonConvert.SerializeObject(EmpresasUsuario);


            var permisosHijosJson = JsonConvert.SerializeObject(permisosHijos);

            
            // Crear el claim con la lista serializada
            var claims = new List<Claim>
            {
                new Claim("id_usuario", usuario.IdUsuario.ToString()),
                new Claim("id_empresa", idempresa ?? string.Empty),
                new Claim("empresas_usuario", empresasUsuarioJson),
                new Claim("correousuario", usuario.CorreoElectronico ?? string.Empty),
                new Claim("nombrecompleto", usuario.Nombre +" "+ usuario.Apellido),
                new Claim("rolusuario", usuario.IdRolNavigation.Rol ?? string.Empty),
                new Claim("permisosJson", permisosHijosJson),
            };

            JwtSecurityToken token = new(
             issuer: helper.Issuer
             , audience: helper.Audience
             , claims: claims
             , expires: DateTime.UtcNow.AddMinutes(43800)
             , notBefore: DateTime.UtcNow
             , signingCredentials:
            new SigningCredentials(this.helper.GetKeyToken(), SecurityAlgorithms.HmacSha256)
            );

            var response = new JwtSecurityTokenHandler().WriteToken(token);

            UsuarioDTO usuar = new UsuarioDTO(); 
            usuar.payload = response; 
            usuar.Email = usuario.CorreoElectronico; 
            usuar.Nombrecompleto = usuario.Nombre +" "+ usuario.Apellido; 
            usuar.Rol = usuario.IdRolNavigation.Rol; 
            usuar.permisos = permisosPadres; 
            

            return new ResponseModel<UsuarioDTO>(true,false, "Credenciales correctas",usuar);

            }
            }
            catch (Exception ex)
            {
                // Maneja la excepción y proporciona información útil para el diagnóstico
                return new ResponseModel<UsuarioDTO>(false,true, $"Error: {ex.Message}");
            }
        }

        
        public async Task<ResponseModel<List<EmpresaDTO>>> ObtenerEmpresasUsuario(int idUsuario)
        {

        try
          {
            var empresasUsuario = await (from eu in _context.TbEmpresasUsuarios
                               join e in _context.TbEmpresas on eu.IdEmpresa equals e.IdEmpresa
                               where eu.IdUsuario == idUsuario
                               select new EmpresaDTO
                               {
                                   NombreEmpresa = e.NombreEmpresa,
                                   IdEmpresa = e.IdEmpresa
                               }).ToListAsync();

            if (empresasUsuario.Any())
               {
                return new ResponseModel<List<EmpresaDTO>>(true,false,"OK", empresasUsuario);
               }
            else
               {
                return new ResponseModel<List<EmpresaDTO>>(true, true, "No se encontraron empresas para el usuario.");
               }

            }
            catch (Exception ex)
            {
                // Maneja la excepción y proporciona información útil para el diagnóstico
                return new ResponseModel<List<EmpresaDTO>>(false,true, $"Error: {ex.Message}");
            }

        }

        public async Task<ResponseModel<UsuarioDTO>>  GetTokenCompany(UserRequest userRequest, int IdEmpresaRequest)
        {

             try
            {

             var idUser = userRequest.IdUsuario;
             var Empresas = userRequest.EmpresasUsuarioJson; 

             if (!string.IsNullOrEmpty(Empresas))
             {
                // Deserializar el JSON de vuelta a una lista de objetos
                var empresasUsuario = JsonConvert.DeserializeObject<List<EmpresaUsuarioDTO>>(Empresas);

                var empresaBuscada = empresasUsuario.FirstOrDefault(e => e.IdEmpresa == IdEmpresaRequest);

                if (empresaBuscada != null)
                {

                    var claims = new List<Claim>
                    {
                        new Claim("id_usuario", idUser ?? string.Empty),
                        new Claim("id_empresa", IdEmpresaRequest.ToString() ?? string.Empty),
                        new Claim("empresas_usuario", Empresas),
                        new Claim("email", userRequest.Correo ?? string.Empty),
                        new Claim("nombrecompleto", userRequest.Nombre ?? string.Empty),
                        new Claim("rolusuario", userRequest.Rol ?? string.Empty),
                        new Claim("permisosJson", userRequest.permisosJson ?? string.Empty),

                    };

                    JwtSecurityToken token = new(
                        issuer: helper.Issuer
                        , audience: helper.Audience
                        , claims: claims
                        ,expires: DateTime.UtcNow.AddMinutes(43800)
                        , notBefore: DateTime.UtcNow
                        , signingCredentials:
                        new SigningCredentials(this.helper.GetKeyToken(), SecurityAlgorithms.HmacSha256)
                        );

                    var response = new JwtSecurityTokenHandler().WriteToken(token);
                    UsuarioDTO usuar = new UsuarioDTO(); 
                    usuar.payload = response; 
                    usuar.Email = userRequest.Correo; 
                    usuar.Nombrecompleto = userRequest.Nombre; 
                    usuar.Rol = userRequest.Rol; 

                    return new ResponseModel<UsuarioDTO>(true,false, "OK",usuar);
           
                }
                else{

                    return new ResponseModel<UsuarioDTO>(true,true, "El usuario no tiene permiso para acceder a esta empresa");

                }


            }
            else
            {
                // Manejar el caso donde el claim no está presente o está vacío
               return new ResponseModel<UsuarioDTO>(true,true, "No se encontraron empresas disponibles para el usuario");
            }
            }

            catch (Exception ex)
            {
                // Maneja la excepción y proporciona información útil para el diagnóstico
                return new ResponseModel<UsuarioDTO>(false,true, $"Error: {ex.Message}");
            }

  

        }
    }


}