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
    public class promocjelokaluController : ControllerBase
    {
        private readonly DataContext _context;

        public promocjelokaluController(DataContext context)
        {
            _context = context;
        }

        // GET: api/promocjelokalu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<promocjelokalu>>> Getpromocjelokalu()
        {
          if (_context.promocjelokalu == null)
          {
              return NotFound();
          }
            return await _context.promocjelokalu.ToListAsync();
        }

        // GET: api/promocjelokalu/5
        [HttpGet("{id}")]
        public async Task<ActionResult<promocjelokalu>> Getpromocjelokalu(int id)
        {
          if (_context.promocjelokalu == null)
          {
              return NotFound();
          }
            var promocjelokalu = await _context.promocjelokalu.FindAsync(id);

            if (promocjelokalu == null)
            {
                return NotFound();
            }

            return promocjelokalu;
        }

        // PUT: api/promocjelokalu/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putpromocjelokalu(int id, promocjelokalu promocjelokalu)
        {
            if (id != promocjelokalu.LokaleId)
            {
                return BadRequest();
            }

            _context.Entry(promocjelokalu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!promocjelokaluExists(id))
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

        // POST: api/promocjelokalu
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<promocjelokalu>> Postpromocjelokalu(promocjelokalu promocjelokalu)
        {
          if (_context.promocjelokalu == null)
          {
              return Problem("Entity set 'DataContext.promocjelokalu'  is null.");
          }
            _context.promocjelokalu.Add(promocjelokalu);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (promocjelokaluExists(promocjelokalu.LokaleId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getpromocjelokalu", new { id = promocjelokalu.LokaleId }, promocjelokalu);
        }

        // DELETE: api/promocjelokalu/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletepromocjelokalu(int id)
        {
            if (_context.promocjelokalu == null)
            {
                return NotFound();
            }
            var promocjelokalu = await _context.promocjelokalu.FindAsync(id);
            if (promocjelokalu == null)
            {
                return NotFound();
            }

            _context.promocjelokalu.Remove(promocjelokalu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool promocjelokaluExists(int id)
        {
            return (_context.promocjelokalu?.Any(e => e.LokaleId == id)).GetValueOrDefault();
        }
    }
}
