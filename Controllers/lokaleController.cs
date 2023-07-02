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
            return await _context.lokale.ToListAsync();
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
        // paramet whetherPromocja określa czy metoda wypiszę lokale z aktywnymi promocjami true=>z false=>obojętnie
        //Parametry begin/endScopeCena określają przedział cenowy, który musi spełnić przynajmniej jedno danie w lokalu, 
        //jeśli będą null to begin. zostanie ustawony na 0 a end. na maksymalną wartość dla doubla 
        // zwraca lokale z daną kuchnią i miastem z przynajmniej jednym daniem z podanego zakresu, w zależności od parametru whetherPromocja, z przynajmniej jedną aktywną promocją 
        [HttpGet("GetlokaleByKuchniaMiastoPromocjaCenaScope/{kuchnia},{miasto},{whetherPromocja},{beginScopeCena},{endScopeCena}")]
        public async Task<ActionResult<IEnumerable<lokale>>> GetlokaleByKuchniaMiastoPromocjaCenaScope(string? kuchnia,string? miasto,bool whetherPromocja,double? beginScopeCena,double? endScopeCena)
        {
            List<lokale> lokaleToReturn = new List<lokale>();
            List<lokale> lokalePromocjeFiltered = new List<lokale>();

            if (_context.lokale == null)
            {
                return NotFound();
                
            }

            //jeśli parametr to null to ustawia go na stringa reprezentującego wszystkie rodzaje kuchni w bazie 
            if (kuchnia == null || kuchnia == "Any") {
                var distinctListKuchnie =  _context.lokale.Select(x => x.Kuchnia).Distinct();
                StringBuilder  builderKuchnia = new StringBuilder();
                foreach (string i in distinctListKuchnie) { 
                builderKuchnia.Append(i);
                } 
                kuchnia =builderKuchnia.ToString();
            }
            //jeśli parametr to null to ustawia go na stringa reprezentującego wszystkie rodzaje miasta w bazie 
            if (miasto == null || miasto=="Any")
            {
                var distinctListMiasta = _context.lokale.Select(x => x.Miasto).Distinct();
                StringBuilder builderMiasto = new StringBuilder();
                foreach (string i in distinctListMiasta)
                {
                    builderMiasto.Append(i);
                }
                miasto = builderMiasto.ToString();
            }


            if (beginScopeCena == null) { beginScopeCena = 0; }
            if (endScopeCena == null) { endScopeCena = Double.MaxValue; }

            var lokale = await _context.lokale.Where(b =>kuchnia.Contains(b.Kuchnia) && miasto.Contains(b.Miasto)).Include(b => b.Promocje).Include(b=>b.Dania).ToListAsync();


            //na bazie listy lokale uzupełnia listę lokalePromocjeFiltered o lokale z aktywnymi promocjami i czyści im listę promocji
            if (whetherPromocja)
            {
                foreach (var lokal in lokale)
                {
                    bool activePromocje = false;
                    foreach (promocje k in lokal.Promocje)
                    {   
                        if (DateTime.Compare(k.DataRozpoczęcia, DateTime.Now) <= 0 && DateTime.Compare(k.DataZakończenia, DateTime.Now) >= 0)
                        {
                            activePromocje=true;
                            break;
                        }
                    }
                    if (activePromocje)
                    {
                        lokal.Promocje.Clear();
                        lokalePromocjeFiltered.Add(lokal);
                    }
                }
            }
            else 
            {
                foreach (var lokal in lokale)
                {
                    lokal.Promocje.Clear();
                    lokalePromocjeFiltered.Add( lokal);
                }
            }
            //________________________

            //na bazie listy lokalePromocjeFiltered uzupełnia listę lokaleToReturn o lokale z przynajmniej jednym daniem w zakresie cenowym i czyści im listę dań
            foreach (lokale lokal in lokalePromocjeFiltered) 
            { 
                bool ifAtLeastOneDanie=false;   
                foreach(dania danie in lokal.Dania)
                { 
                    if(danie.Cena>=beginScopeCena && danie.Cena <= endScopeCena)
                    {
                        ifAtLeastOneDanie = true; 
                        break;
                    }  
                }
                if (ifAtLeastOneDanie)
                {
                    lokal.Dania.Clear();
                    lokaleToReturn.Add(lokal);
                }
            }

            if (lokaleToReturn == null)
            {
                return NotFound();
            }

            return lokaleToReturn;
        }
        
        
        //zwraca listę różnych miast w, których są lokale     
        [HttpGet("GetMiasta")]
        public async Task<ActionResult<IEnumerable<string>>> GetMiasta() {
            if (_context.lokale == null)
            {
                return NotFound();

            }
            return await _context.lokale.Select(x => x.Miasto).Distinct().ToListAsync();
        }
        
        
        //zwraca listę różnych kuchni w lokalach
        [HttpGet("GetKuchnie")]
        public async Task<ActionResult<IEnumerable<string>>> GetKuchnie()
        {
            if (_context.lokale == null)
            {
                return NotFound();

            }
            return await _context.lokale.Select(x => x.Kuchnia).Distinct().ToListAsync();
        }

       
        
        private bool lokaleExists(int id)
        {
            return (_context.lokale?.Any(e => e.LokaleId == id)).GetValueOrDefault();
        }
    }
}
