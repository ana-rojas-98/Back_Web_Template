using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StrategicviewBack.Models;
using StrategicviewBack.Models.DTO;

namespace StrategicviewBack.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly Way2godbContext _context;

        public AuthController(Way2godbContext context)
        {
            _context = context;
        }

        // POST: api/Auth
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TbUsuario>> Postlogin(Login login)
        {
            var usuario = await _context.TbUsuarios.Where(usuario =>
                usuario.CorreoElectronico == login.Email &&
                usuario.Password == login.Password
            ).FirstOrDefaultAsync();

            return StatusCode(StatusCodes.Status200OK, new { id = usuario.Username, usuario });
        }

    }
}
