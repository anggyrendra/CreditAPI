using CreditAPI.Data;
using CreditAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CreditAPI.Services
{
    public class AngsuranService
    {
        private readonly ApplicationDbContext _context;

        public AngsuranService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Angsuran> TambahAngsuran(Guid kreditId, decimal jumlah, DateTime jatuhTempo)
        {
            var angsuran = new Angsuran
            {
                KreditId = kreditId,
                Jumlah = jumlah,
                SudahDibayar = 0,
                JatuhTempo = jatuhTempo,
                Lunas = false
            };

            _context.Angsurans.Add(angsuran);
            await _context.SaveChangesAsync();
            return angsuran;
        }

        public async Task<Angsuran> BayarAngsuran(Guid angsuranId, decimal nominal)
        {
            var angsuran = await _context.Angsurans.FirstOrDefaultAsync(a => a.Id == angsuranId);

            if (angsuran == null)
                throw new Exception("Angsuran tidak ditemukan.");

            angsuran.SudahDibayar += nominal;

            if (angsuran.SudahDibayar >= angsuran.Jumlah)
                angsuran.Lunas = true;

            await _context.SaveChangesAsync();
            return angsuran;
        }

        public async Task<List<Angsuran>> GetByKreditId(Guid kreditId)
        {
            return await _context.Angsurans
                .Where(a => a.KreditId == kreditId)
                .ToListAsync();
        }
    }
}
