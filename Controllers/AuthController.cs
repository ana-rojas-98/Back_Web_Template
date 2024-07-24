using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StrategicviewBack.Logic;
using StrategicviewBack.Models;
using StrategicviewBack.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using StrategicviewBack.Utilities; 

namespace StrategicviewBack.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly AuthLogic _logic;

        public AuthController(AuthLogic logic)
        {
            _logic = logic;
        }

        // POST: api/Auth
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TbUsuario>> Postlogin(LoginDTO login)
        {
            var usuario = await _logic.Authenticate(login);

            return StatusCode(StatusCodes.Status200OK, usuario);
        }


     
        [HttpGet("Get-User-Empresas")]
        [Authorize]
        public async Task<IActionResult> ObtenerEmpresasUsuario()
        {
            var idUser = Convert.ToInt16(HttpContext.User.FindFirst("id_usuario")?.Value);

            var result = await _logic.ObtenerEmpresasUsuario(idUser);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("Get-token-Empresa/{idEmpresaRequest}")]
        [Authorize]
        public async Task<IActionResult> GetLoginEmpresas(int idEmpresaRequest)
        {

            var userRequest = UserUtility.GetUserRequestFromClaims(HttpContext);

            var result = await _logic.GetTokenCompany(userRequest, idEmpresaRequest); 

            return StatusCode(StatusCodes.Status200OK, result);
        }

    }
}
