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
        public async Task<ActionResult<User>> Login(LoginRequest login)
        {
            var user = await _logic.Authenticate(login);

            return StatusCode(StatusCodes.Status200OK, user);
        }


     
        [HttpGet("Get-User-Companies")]
        [Authorize]
        public async Task<IActionResult> GetUserCompanies()
        {
            var userId = Convert.ToInt16(HttpContext.User.FindFirst("user_id")?.Value);

            var result = await _logic.GetUserCompanies(userId);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("Get-token-Company/{companyId}")]
        [Authorize]
        public async Task<IActionResult> GetCompanyToken(int companyId)
        {

            var userRequest = UserUtility.GetAuthenticatedUserRequestFromClaims(HttpContext);

            var result = await _logic.GetTokenCompany(userRequest, companyId); 

            return StatusCode(StatusCodes.Status200OK, result);
        }

    }
}
