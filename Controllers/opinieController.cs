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

    public class opinieController : ControllerBase
    {

        private readonly DataContext _context;

        public opinieController(DataContext context)
        {
            _context = context;
        }

        // GET: api/opinie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<opinie>>> Getopinie()
        {
          if (_context.opinie == null)
          {
              return NotFound();
          }
            return await _context.opinie.Include(_=>_.Lokale).ToListAsync();
        }
        
        // GET: api/opinie/5
        [HttpGet("{id}")]
        public async Task<ActionResult<opinie>> Getopinie(int id)
        {
          if (_context.opinie == null)
          {
              return NotFound();
          }
            var opinie = await _context.opinie.FindAsync(id);

            if (opinie == null)
            {
                return NotFound();
            }

            return opinie;
        }
        
        //zwraca liste opini dla danego Id lokalu 
        [HttpGet("GetopiniebyLokaleId/{LokaleId}")]
        public async Task<ActionResult<IEnumerable<opinie>>> GetopiniebyLokaleId(int LokaleId)
        {
            if (_context.opinie == null)
            {
                return NotFound();
            }
            var opinie = await _context.opinie.Where(b => b.LokaleId == LokaleId).ToListAsync();

            if (opinie == null)
            {
                return NotFound();
            }
            
            return opinie;
        }
        private bool opinieExists(int id)
        {
            return (_context.opinie?.Any(e => e.OpinieId == id)).GetValueOrDefault();
        }
    }
}
