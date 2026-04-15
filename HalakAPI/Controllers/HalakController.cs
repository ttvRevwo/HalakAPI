using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HalakAPI.Models;
using HalakAPI.DTOs;

namespace HalakAPI.Controllers
{
    public class HalakController : Controller
    {
        private readonly HalakContext _context;

        public HalakController(HalakContext context)
        {
            _context = context;
        }

        [HttpGet("/FajMeretTo")]
        
        public IActionResult GetFajMeretTo()
        {
            try
            {
                var halak = _context.Halaks.Select(h => new
                {
                    h.Faj,
                    h.MeretCm,
                    To = h.To == null ? null : new
                    {
                        h.To.Nev
                    }
                }).ToList();
                return StatusCode(200, halak);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Hiba: {ex.Message}");
            }
        }

        [HttpPost("UjHal")]
        public IActionResult UjHal([FromBody] Halak halak)
        {
            if (halak == null)
            {
                return StatusCode(400, "Üres objektum nem rögzíthető!");
            }

            try
            {
                _context.Halaks.Add(halak);
                _context.SaveChanges();
                return Ok("Sikeres rögzítés!");
            }
            catch (Exception ex)
            {
                return StatusCode(400, "Hiba: " + ex.Message);
            }
        }

        [HttpPut("/ModositHal")]
        public IActionResult Modosit(Halak halak)
        {
            try
            {
                var regiHal = _context.Halaks.FirstOrDefault(x => x.Id == halak.Id);
                if (regiHal == null)
                    return NotFound("Nincs ilyen azonosítójú hal.");

                regiHal.Faj = halak.Faj;
                regiHal.MeretCm = halak.MeretCm;
                regiHal.To = halak.To;
                _context.SaveChanges();

                return Ok("Sikeres módosítás.");
            }
            catch (Exception ex)
            {
                return StatusCode(400, "Hiba: " + ex.Message);
            }
        }

        [HttpDelete("/TorolHal/{id}")]
        public IActionResult Torol(int id)
        {
            try
            {
                var torlendo = _context.Halaks.FirstOrDefault(x => x.Id == id);

                if (torlendo == null)
                    return NotFound("Nincs ilyen azonosítójú hal!");

                _context.Halaks.Remove(torlendo);
                _context.SaveChanges();

                return Ok("Sikeres törlés.");
            }
            catch (Exception ex)
            {
                return StatusCode(400, "Hiba: " + ex.Message);
            }
        }
    }
}