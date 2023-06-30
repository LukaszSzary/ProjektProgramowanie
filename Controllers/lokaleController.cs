using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        // przyjmije jako parametry złożonego z nazw kuchni, miasta  np. WłoskaIndyjskaPolska 
        // Parametr "Any" reprezentuje wszystkie miasta/kuchnie w bazie
        // zwraca lokale z dana kuchnią i miastem 
        [HttpGet("GetlokaleByKuchniaMiasto/{kuchnia},{miasto}")]
        public async Task<ActionResult<IEnumerable<lokale>>> GetlokaleByKuchniaMiasto(string? kuchnia,string? miasto)
        {
            if (_context.lokale == null)
            {
                return NotFound();
                
            }

            //jeśli parametr to null to ustawia go na stringa reprezentującego wszystkie rodzaje kuchni w bazie 
            if (kuchnia == null || kuchnia == "Any") {
               var DistinctListKuchnie= _context.lokale.Select(x => x.Kuchnia).Distinct();
                StringBuilder  builderKuchnia = new StringBuilder();
                foreach (string i in DistinctListKuchnie) { 
                builderKuchnia.Append(i);
                } 
                kuchnia =builderKuchnia.ToString();
            }
            //jeśli parametr to null to ustawia go na stringa reprezentującego wszystkie rodzaje miasta w bazie 
            if (miasto == null || miasto=="Any")
            {
                var DistinctListMiasta = _context.lokale.Select(x => x.Miasto).Distinct();
                StringBuilder builderMiasto = new StringBuilder();
                foreach (string i in DistinctListMiasta)
                {
                    builderMiasto.Append(i);
                }
                miasto = builderMiasto.ToString();
            }
;
            var lokale = await _context.lokale.Where(b =>kuchnia.Contains(b.Kuchnia) && miasto.Contains(b.Miasto)).ToListAsync();
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
