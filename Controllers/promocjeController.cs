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

        //Zwraca listę obowiązujących promocji dla danego id lokalu
        [HttpGet("GetPromocjeByLokaleId/{id}")]
        public async Task<ActionResult<IEnumerable<promocje>>> GetPromocjeByLokaleId(int id)
        {
            List<promocje> promocjeToreturn = new List<promocje>();
            if (_context.lokale == null)
            {
                return NotFound();
            }

            var lokal = await _context.lokale.Where(b=>b.LokaleId==id).Include(b => b.Promocje).ToListAsync();

            if (lokal == null || lokal[0].Promocje == null)
            {
                return NotFound();
            }

            //na podstawie promocji lokalu uzupełnie promocjeToReturn aktualnymi promocjami
            foreach (promocje promocja in lokal[0].Promocje) 
            {
                if (DateTime.Compare(promocja.DataRozpoczęcia, DateTime.Now) <= 0 && DateTime.Compare(promocja.DataZakończenia, DateTime.Now) >= 0)
                {
                    promocja.Lokale.Clear();
                    promocjeToreturn.Add(promocja);
                }
            }

            if (promocjeToreturn==null)
            {
                return NotFound();
            }

            return promocjeToreturn;
        }

        private bool promocjeExists(int id)
        {
            return (_context.promocje?.Any(e => e.PromocjeId == id)).GetValueOrDefault();
        }
    }
}
