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
    public class promocjeController : ControllerBase
    {
        private readonly DataContext _context;

        public promocjeController(DataContext context)
        {
            _context = context;
        }

        // GET: api/promocje
        [HttpGet]
        public async Task<ActionResult<IEnumerable<promocje>>> Getpromocje()
        {
          if (_context.promocje == null)
          {
              return NotFound();
          }
            return await _context.promocje.ToListAsync();
        }

        // GET: api/promocje/5
        [HttpGet("{id}")]
        public async Task<ActionResult<promocje>> Getpromocje(int id)
        {
          if (_context.promocje == null)
          {
              return NotFound();
          }
            var promocje = await _context.promocje.FindAsync(id);

            if (promocje == null)
            {
                return NotFound();
            }

            return promocje;
        }

        // PUT: api/promocje/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putpromocje(int id, promocje promocje)
        {
            if (id != promocje.PromocjeId)
            {
                return BadRequest();
            }

            _context.Entry(promocje).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!promocjeExists(id))
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

        // POST: api/promocje
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<promocje>> Postpromocje(promocje promocje)
        {
          if (_context.promocje == null)
          {
              return Problem("Entity set 'DataContext.promocje'  is null.");
          }
            _context.promocje.Add(promocje);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getpromocje", new { id = promocje.PromocjeId }, promocje);
        }

        // DELETE: api/promocje/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletepromocje(int id)
        {
            if (_context.promocje == null)
            {
                return NotFound();
            }
            var promocje = await _context.promocje.FindAsync(id);
            if (promocje == null)
            {
                return NotFound();
            }

            _context.promocje.Remove(promocje);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool promocjeExists(int id)
        {
            return (_context.promocje?.Any(e => e.PromocjeId == id)).GetValueOrDefault();
        }
    }
}
