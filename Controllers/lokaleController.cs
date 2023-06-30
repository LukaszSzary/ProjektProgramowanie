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

        //zwraca lokale z daną kuchnią
        [HttpGet("GetlokaleByKuchnia/{kuchnia}")]
        public async Task<ActionResult<IEnumerable<lokale>>> GetlokaleByKuchnia(String kuchnia)
        {
            if (_context.lokale == null)
            {
                return NotFound();
            }
            var lokale = await _context.lokale.Where(b => b.Kuchnia == kuchnia).ToListAsync();

            if (lokale == null)
            {
                return NotFound();
            }

            return lokale;
        }

        private bool lokaleExists(int id)
        {
            return (_context.lokale?.Any(e => e.LokaleId == id)).GetValueOrDefault();
        }
    }
}
