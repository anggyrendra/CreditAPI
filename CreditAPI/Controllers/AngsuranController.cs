using Microsoft.AspNetCore.Mvc;
using CreditAPI.Data;
using CreditAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CreditAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AngsuranController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AngsuranController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.Angsurans
                .Include(a => a.Kredit)
                .ToListAsync();

            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var angsuran = await _context.Angsurans
                .Include(a => a.Kredit)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (angsuran == null)
                return NotFound(new { message = "Angsuran tidak ditemukan" });

            return Ok(angsuran);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Angsuran angsuran)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            angsuran.Id = Guid.NewGuid();
            angsuran.CreatedAt = DateTime.UtcNow;

            _context.Angsurans.Add(angsuran);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = angsuran.Id }, angsuran);
        }

        [HttpPut("{id}/bayar")]
        public async Task<IActionResult> BayarAngsuran(Guid id, [FromBody] decimal jumlahBayar)
        {
            var angsuran = await _context.Angsurans.FirstOrDefaultAsync(a => a.Id == id);

            if (angsuran == null)
                return NotFound(new { message = "Angsuran tidak ditemukan" });

            if (angsuran.StatusLunas)
                return BadRequest(new { message = "Angsuran sudah lunas" });

            angsuran.SudahDibayar += jumlahBayar;
            angsuran.JumlahBayar = jumlahBayar;
            angsuran.TanggalBayar = DateTime.UtcNow;

            if (angsuran.SudahDibayar >= angsuran.Jumlah)
            {
                angsuran.StatusLunas = true;
            }

            _context.Angsurans.Update(angsuran);
            await _context.SaveChangesAsync();

            return Ok(angsuran);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var angsuran = await _context.Angsurans.FirstOrDefaultAsync(a => a.Id == id);
            if (angsuran == null)
                return NotFound(new { message = "Angsuran tidak ditemukan" });

            _context.Angsurans.Remove(angsuran);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
