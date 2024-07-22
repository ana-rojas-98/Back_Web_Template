using StrategicviewBack.Models;
using StrategicviewBack.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using static IterumBackend.Token;

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


        public async Task<dynamic> Authenticate(LoginDTO login)
        {

             var usuario = await _context.TbUsuarios.Where(usuario =>
                usuario.CorreoElectronico == login.Email &&
                usuario.Password == login.Password
            ).FirstOrDefaultAsync();

            if( usuario == null)
            {
              return new ResponseModel<TbUsuario>(true, "Credenciales incorrectas");
            }

            JwtSecurityToken token = new(
             issuer: helper.Issuer
             , audience: helper.Audience
             , claims: []
             , expires: DateTime.UtcNow.AddMinutes(43800)
             , notBefore: DateTime.UtcNow
             , signingCredentials:
            new SigningCredentials(this.helper.GetKeyToken(), SecurityAlgorithms.HmacSha256)
            );

            var response = new JwtSecurityTokenHandler().WriteToken(token);

            UsuarioDTO usuar = new UsuarioDTO(); 
            usuar.payload = response; 

            return new ResponseModel<UsuarioDTO>(true, "Credenciales correctas",usuar);


        }



    }


}