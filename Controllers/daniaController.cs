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

        //Zwraca listę dań dla danego id lokalu
        [HttpGet("GetDaniaByLokaleId/{id}")]
        public async Task<ActionResult<IEnumerable<dania>>> GetDaniaByLokaleId(int id)
        {
            List<dania> daniaToReturn = new List<dania>();
            if (_context.dania == null)
            {
                return NotFound();
            }

            var dania = await _context.dania.Include(b => b.Lokale).ToListAsync();

            foreach (dania danie in dania)
            {
                bool ifIdLokalMaches = false;
                foreach (lokale lokal in danie.Lokale)
                {
                    if (lokal.LokaleId == id)
                    {
                        ifIdLokalMaches = true;
                        break;
                    }
                }
                if (ifIdLokalMaches)
                {
                    danie.Lokale.Clear();
                    daniaToReturn.Add(danie);
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
