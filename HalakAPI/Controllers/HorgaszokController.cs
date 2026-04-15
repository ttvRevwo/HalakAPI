using HalakAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HalakAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HorgaszokController : ControllerBase
    {

        private readonly HalakContext _context;

        public HorgaszokController(HalakContext context)
        {
            _context = context;
        }


        [HttpGet("/All")]
        public IActionResult GetAllHorgaszok()
        {
            try
            {
                var turak = _context.Horgaszoks.ToList();
                return StatusCode(200, turak);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Hiba: {ex.Message}");
            }
        }

        [HttpGet("ById/{id}")]
        public IActionResult ById(int id)
        {
            try
            {
                var horgasz = _context.Horgaszoks.FirstOrDefault(h => h.Id == id);

                if (horgasz == null)
                {
                    return NotFound($"Nincs ilyen id-jű horgász: {id}");
                }

                return Ok(horgasz);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Hiba: {ex.Message}");
            }
        }
    }
}
