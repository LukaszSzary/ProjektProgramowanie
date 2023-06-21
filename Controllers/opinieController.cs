using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektProgramowanie;
using ProjektProgramowanie.Model;

namespace ProjektProgramowanie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class opinieController : ControllerBase
    {
        private readonly DataContext _context;

        public opinieController(DataContext context)
        {
            _context = context;
        }

        // GET: api/opinie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<opinie>>> Getopinie()
        {
          if (_context.opinie == null)
          {
              return NotFound();
          }
            return await _context.opinie.ToListAsync();
        }

        // GET: api/opinie/5
        [HttpGet("{id}")]
        public async Task<ActionResult<opinie>> Getopinie(int id)
        {
          if (_context.opinie == null)
          {
              return NotFound();
          }
            var opinie = await _context.opinie.FindAsync(id);

            if (opinie == null)
            {
                return NotFound();
            }

            return opinie;
        }

        // PUT: api/opinie/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putopinie(int id, opinie opinie)
        {
            if (id != opinie.OpinieId)
            {
                return BadRequest();
            }

            _context.Entry(opinie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!opinieExists(id))
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

        // POST: api/opinie
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<opinie>> Postopinie(opinie opinie)
        {
          if (_context.opinie == null)
          {
              return Problem("Entity set 'DataContext.opinie'  is null.");
          }
            _context.opinie.Add(opinie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getopinie", new { id = opinie.OpinieId }, opinie);
        }

        // DELETE: api/opinie/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteopinie(int id)
        {
            if (_context.opinie == null)
            {
                return NotFound();
            }
            var opinie = await _context.opinie.FindAsync(id);
            if (opinie == null)
            {
                return NotFound();
            }

            _context.opinie.Remove(opinie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool opinieExists(int id)
        {
            return (_context.opinie?.Any(e => e.OpinieId == id)).GetValueOrDefault();
        }
    }
}
