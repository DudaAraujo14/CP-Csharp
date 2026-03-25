using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BrinquedosAPI.Data;
using BrinquedosAPI.Models;

namespace BrinquedosAPI.Controllers
{
    [ApiController]
    [Route("brinquedos")]
    public class BrinquedosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BrinquedosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brinquedo>>> Get()
        {
            return await _context.Brinquedos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Brinquedo>> GetById(int id)
        {
            var brinquedo = await _context.Brinquedos.FindAsync(id);

            if (brinquedo == null)
                return NotFound();

            return brinquedo;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Brinquedo brinquedo)
        {
            _context.Brinquedos.Add(brinquedo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = brinquedo.Id_brinquedo }, brinquedo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Brinquedo brinquedo)
        {
            if (id != brinquedo.Id_brinquedo)
                return BadRequest();

            _context.Entry(brinquedo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var brinquedo = await _context.Brinquedos.FindAsync(id);

            if (brinquedo == null)
                return NotFound();

            _context.Brinquedos.Remove(brinquedo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}