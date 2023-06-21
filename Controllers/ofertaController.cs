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
    public class ofertaController : ControllerBase
    {
        private readonly DataContext _context;

        public ofertaController(DataContext context)
        {
            _context = context;
        }

        // GET: api/oferta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<oferta>>> Getoferty()
        {
          if (_context.oferta == null)
          {
              return NotFound();
          }
            return await _context.oferta.ToListAsync();
        }

        // GET: api/oferta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<oferta>> Getoferta(int id)
        {
          if (_context.oferta == null)
          {
              return NotFound();
          }
            var oferta = await _context.oferta.FindAsync(id);

            if (oferta == null)
            {
                return NotFound();
            }

            return oferta;
        }

        // PUT: api/oferta/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putoferta(int id, oferta oferta)
        {
            if (id != oferta.LokaleId)
            {
                return BadRequest();
            }

            _context.Entry(oferta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ofertaExists(id))
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

        // POST: api/oferta
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<oferta>> Postoferta(oferta oferta)
        {
          if (_context.oferta == null)
          {
              return Problem("Entity set 'DataContext.oferta'  is null.");
          }
            _context.oferta.Add(oferta);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ofertaExists(oferta.LokaleId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getoferta", new { id = oferta.LokaleId }, oferta);
        }

        // DELETE: api/oferta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteoferta(int id)
        {
            if (_context.oferta == null)
            {
                return NotFound();
            }
            var oferta = await _context.oferta.FindAsync(id);
            if (oferta == null)
            {
                return NotFound();
            }

            _context.oferta.Remove(oferta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ofertaExists(int id)
        {
            return (_context.oferta?.Any(e => e.LokaleId == id)).GetValueOrDefault();
        }
    }
}
