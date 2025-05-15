using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Amigos.DataAccessLayer;
using Amigos.Models.TuProyecto.Models;

namespace Amigos.Controllers
{
    [ApiController]
    [Route("api/amigo")]
    public class AmigoAPIController : Controller
    {
        
        private readonly AmigoDBContext _context;

        public AmigoAPIController(AmigoDBContext context)
        {
            _context = context;
        }

        // GET: api/amigo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Amigo>>> GetAmigos()
        {
            return await _context.Amigos.ToListAsync();
        }

        // GET: api/amigo/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Amigo>> GetAmigo(int id)
        {
            var amigo = await _context.Amigos.FindAsync(id);

            if (amigo == null)
            {
                return NotFound();  // Devuelve 404 si no se encuentra el amigo
            }

            return amigo;  // Devuelve el amigo en formato JSON
        }

        // POST: api/amigo
        [HttpPost]
        public async Task<ActionResult<Amigo>> PostAmigo(Amigo amigo)
        {
            _context.Amigos.Add(amigo);
            await _context.SaveChangesAsync();

            // Devuelve 201 Created y la URL del nuevo recurso
            return CreatedAtAction(nameof(GetAmigo), new { id = amigo.ID }, amigo);
        }

        // PUT: api/amigo/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmigo(int id, Amigo amigo)
        {
            if (id != amigo.ID)
            {
                return BadRequest();  // Devuelve 400 si el ID no coincide
            }

            _context.Entry(amigo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();  // Devuelve 204 No Content cuando la actualización es exitosa
        }
        // DELETE: api/amigo/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmigo(int id)
        {
            var amigo = await _context.Amigos.FindAsync(id);
            if (amigo == null)
            {
                return NotFound();  // Devuelve 404 si no se encuentra el amigo
            }

            _context.Amigos.Remove(amigo);
            await _context.SaveChangesAsync();

            return NoContent();  // Devuelve 204 No Content cuando el recurso ha sido eliminado
        }
    }
}
