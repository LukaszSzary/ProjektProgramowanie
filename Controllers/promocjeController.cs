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

        private bool promocjeExists(int id)
        {
            return (_context.promocje?.Any(e => e.PromocjeId == id)).GetValueOrDefault();
        }
    }
}
