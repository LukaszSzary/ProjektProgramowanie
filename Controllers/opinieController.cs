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
            return await _context.opinie.ToListAsync();
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
        [HttpGet("GetOpiniebyLokaleId/{lokaleId}")]
        public async Task<ActionResult<IEnumerable<opinie>>> GetOpiniebyLokaleId(int lokaleId)
        {
            List<opinie> opinieToReturn = new List<opinie>();
            if (_context.opinie == null)
            {
                return NotFound();
            }
            var opinie = await _context.opinie.Where(b => b.LokaleId == lokaleId).ToListAsync();

            if (opinie == null)
            {
                return NotFound();
            }

            return opinie;
        }
        //zwraca średnią wartość(jako string) ocen dla lokalu o danym id
        [HttpGet("GetAvgOceneByLokaleId/{lokaleId}")]
        public async Task<ActionResult<string>> GetAvgOceneByLokaleId(int lokaleId)
        {
            double avgOcena = 0.00d;
            if (_context.opinie == null)
            {
                return NotFound();
            }
            var opinie = await _context.opinie.Where(b => b.LokaleId == lokaleId).ToListAsync();

            if (opinie == null )
            {
                return NotFound();
            }

            foreach (opinie op in opinie)
            {
                avgOcena += op.Ocena;
            }
            avgOcena = avgOcena / opinie.Count();
            return String.Format("{0:0.##}", avgOcena); ;
        }
      
        private bool opinieExists(int id)
        {
            return (_context.opinie?.Any(e => e.OpinieId == id)).GetValueOrDefault();
        }
    }
}
