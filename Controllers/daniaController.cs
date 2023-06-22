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
    public class daniaController : ControllerBase
    {
        private readonly DataContext _context;

        public daniaController(DataContext context)
        {
            _context = context;
        }

        // GET: api/dania
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dania>>> Getdania()
        {
          if (_context.dania == null)
          {
              return NotFound();
          }
            return await _context.dania.Include(_ => _.Lokale).ToListAsync();
        }

        // GET: api/dania/5
        [HttpGet("{id}")]
        public async Task<ActionResult<dania>> Getdania(int id)
        {
          if (_context.dania == null)
          {
              return NotFound();
          }
            var dania = await _context.dania.FindAsync(id);

            if (dania == null)
            {
                return NotFound();
            }

            return dania;
        }

        // PUT: api/dania/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putdania(int id, dania dania)
        {
            if (id != dania.DaniaId)
            {
                return BadRequest();
            }

            _context.Entry(dania).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!daniaExists(id))
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

        // POST: api/dania
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<dania>> Postdania(dania dania)
        {
          if (_context.dania == null)
          {
              return Problem("Entity set 'DataContext.dania'  is null.");
          }
            _context.dania.Add(dania);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getdania", new { id = dania.DaniaId }, dania);
        }

        // DELETE: api/dania/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletedania(int id)
        {
            if (_context.dania == null)
            {
                return NotFound();
            }
            var dania = await _context.dania.FindAsync(id);
            if (dania == null)
            {
                return NotFound();
            }

            _context.dania.Remove(dania);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool daniaExists(int id)
        {
            return (_context.dania?.Any(e => e.DaniaId == id)).GetValueOrDefault();
        }
    }
}
