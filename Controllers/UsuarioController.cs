using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StrategicviewBack.Models;
using Microsoft.AspNetCore.Authorization;

namespace StrategicviewBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly Way2godbContext _context;

        public UsuarioController(Way2godbContext context)
        {
            _context = context;
        }

        // GET: api/Usuario
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TbUsuario>>> GetTbUsuarios()
        {
    
            return await _context.TbUsuarios.ToListAsync();
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TbUsuario>> GetTbUsuario(int id)
        {
            var tbUsuario = await _context.TbUsuarios.FindAsync(id);

            if (tbUsuario == null)
            {
                return NotFound();
            }

            return tbUsuario;
        }

        // PUT: api/Usuario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTbUsuario(int id, TbUsuario tbUsuario)
        {
            if (id != tbUsuario.IdUsuario)
            {
                return BadRequest();
            }

            _context.Entry(tbUsuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TbUsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Usuario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TbUsuario>> PostTbUsuario(TbUsuario tbUsuario)
        {
            _context.TbUsuarios.Add(tbUsuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTbUsuario", new { id = tbUsuario.IdUsuario }, tbUsuario);
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTbUsuario(int id)
        {
            var tbUsuario = await _context.TbUsuarios.FindAsync(id);
            if (tbUsuario == null)
            {
                return NotFound();
            }

            _context.TbUsuarios.Remove(tbUsuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TbUsuarioExists(int id)
        {
            return _context.TbUsuarios.Any(e => e.IdUsuario == id);
        }
    }
}
