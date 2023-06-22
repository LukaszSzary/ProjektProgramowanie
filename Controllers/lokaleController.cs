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
    public class lokaleController : ControllerBase
    {
        private readonly DataContext _context;

        public lokaleController(DataContext context)
        {
            _context = context;
        }

        // GET: api/lokale
        [HttpGet]
        public async Task<ActionResult<IEnumerable<lokale>>> Getlokale()
        {
          if (_context.lokale == null)
          {
              return NotFound();
          }
            return await _context.lokale.Include(_=>_.Opinie).Include(_ => _.Dania).Include(_=>_.Promocje).ToListAsync();
        }

        // GET: api/lokale/5
        [HttpGet("{id}")]
        public async Task<ActionResult<lokale>> Getlokale(int id)
        {
          if (_context.lokale == null)
          {
              return NotFound();
          }
            var lokale = await _context.lokale.FindAsync(id);

            if (lokale == null)
            {
                return NotFound();
            }

            return lokale;
        }

        // PUT: api/lokale/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putlokale(int id, lokale lokale)
        {
            if (id != lokale.LokaleId)
            {
                return BadRequest();
            }

            _context.Entry(lokale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!lokaleExists(id))
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

        // POST: api/lokale
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<lokale>> Postlokale(lokale lokale)
        {
          if (_context.lokale == null)
          {
              return Problem("Entity set 'DataContext.lokale'  is null.");
          }
            _context.lokale.Add(lokale);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getlokale", new { id = lokale.LokaleId }, lokale);
        }

        // DELETE: api/lokale/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletelokale(int id)
        {
            if (_context.lokale == null)
            {
                return NotFound();
            }
            var lokale = await _context.lokale.FindAsync(id);
            if (lokale == null)
            {
                return NotFound();
            }

            _context.lokale.Remove(lokale);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool lokaleExists(int id)
        {
            return (_context.lokale?.Any(e => e.LokaleId == id)).GetValueOrDefault();
        }
    }
}
