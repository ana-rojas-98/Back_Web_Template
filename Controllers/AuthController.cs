using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StrategicviewBack.Logic;
using StrategicviewBack.Models;
using StrategicviewBack.Models.DTO;

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

    }
}
