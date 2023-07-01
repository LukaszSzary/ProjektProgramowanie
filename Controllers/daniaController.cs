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
            return await _context.dania.ToListAsync();
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

        //Zwraca średnią cenę(jako string) dań dla danego id lokalu
        [HttpGet("GetAvdCenaForLokal/{id}")]
        public async Task<ActionResult<string>> GetAvdCenaForLokal(int id)
        {
            double avgCena = 0.00d;
            int howMany = 0;
            if (_context.lokale == null)
            {
                return NotFound();
            }
            var lokal = await _context.lokale.Where(b => b.LokaleId == id).Include(b => b.Dania).ToListAsync();

            if (lokal == null )
            {
                return NotFound();
            }

            foreach (lokale loc in lokal)
            {
                foreach (var da in loc.Dania)
                {
                    avgCena += da.Cena;
                    howMany++;
                }
            }
            avgCena = avgCena / howMany;
            return String.Format("{0:0.00}", avgCena); ;
        }
        
        //Zwraca listę dań dla danego id lokalu
        [HttpGet("GetDaniaByLokaleId/{id}")]
        public async Task<ActionResult<IEnumerable<dania>>> GetDaniaByLokaleId(int id)
        {
            List<dania> daniaToReturn = new List<dania>();
            if (_context.lokale == null)
            {
                return NotFound();
            }

            var lokal = await  _context.lokale.Where(b => b.LokaleId == id).Include(b=>b.Dania).ToListAsync();

            if ( lokal==null  )
            {
                return NotFound();
            }
            
            foreach(lokale loc in lokal)
            {
                foreach (var da in loc.Dania)
                {
                    da.Lokale.Clear();
                    daniaToReturn.Add(da);
                }
            }
            if (daniaToReturn == null)
            {
                return NotFound();
            }
            return daniaToReturn;
        }


        private bool daniaExists(int id)
        {
            return (_context.dania?.Any(e => e.DaniaId == id)).GetValueOrDefault();
        }
    }
}
