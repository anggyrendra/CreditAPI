using CreditAPI.Data;
using CreditAPI.DTOs;
using CreditAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditAPI.Services
{
    public class KreditService : IKreditService
    {


        public Kredit AddKredit(Kredit kredit)
        {
            _context.Kredits.Add(kredit);
            _context.SaveChanges();
            return kredit;
        }

        public Kredit? GetKredit(Guid id) => _context.Kredits.Find(id);

      

        private readonly ApplicationDbContext _context;

        public KreditService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<KreditResponseDto> CreateKredit(KreditCreateDto dto)
        {
            if (dto.Plafon <= 0 || dto.Bunga <= 0 || dto.Bunga > 100 || dto.Tenor <= 0)
                throw new ArgumentException("Input tidak valid");

            var angsuran = CalculateAngsuran(dto.Plafon, dto.Bunga, dto.Tenor);

            var kredit = new Kredit
            {
                Plafon = dto.Plafon,
                Bunga = dto.Bunga,
                Tenor = dto.Tenor,
                Angsuran = angsuran,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Kredits.Add(kredit);
            await _context.SaveChangesAsync();

            return new KreditResponseDto
            {
                Id = kredit.Id,
                Plafon = kredit.Plafon,
                Bunga = kredit.Bunga,
                Tenor = kredit.Tenor,
                Angsuran = kredit.Angsuran
            };
        }

        public async Task<bool> DeleteKredit(Guid id)
        {
            var kredit = await _context.Kredits.FindAsync(id);
            if (kredit == null) throw new KeyNotFoundException("Data tidak ditemukan");

            _context.Kredits.Remove(kredit);
            await _context.SaveChangesAsync();
            return true;
        }

        public decimal CalculateAngsuran(decimal plafon, decimal bunga, int tenor)
        {
            decimal monthlyRate = (bunga / 100) / 12;
            decimal angsuran = plafon * monthlyRate / (1 - (decimal)Math.Pow(1 + (double)monthlyRate, -tenor));
            return Math.Round(angsuran, 2);
        }

        public async Task<IEnumerable<KreditResponseDto>> GetAllKredits()
        {
            return await _context.Kredits
                .Select(k => new KreditResponseDto
                {
                    Id = k.Id,
                    Plafon = k.Plafon,
                    Bunga = k.Bunga,
                    Tenor = k.Tenor,
                    Angsuran = k.Angsuran
                }).ToListAsync();
        }

        public async Task<KreditResponseDto> GetKreditById(Guid id)
        {
            var kredit = await _context.Kredits.FindAsync(id);
            if (kredit == null) throw new KeyNotFoundException("Data tidak ditemukan");

            return new KreditResponseDto
            {
                Id = kredit.Id,
                Plafon = kredit.Plafon,
                Bunga = kredit.Bunga,
                Tenor = kredit.Tenor,
                Angsuran = kredit.Angsuran
            };
        }

        public async Task<KreditResponseDto> UpdateKredit(Guid id, KreditUpdateDto dto)
        {
            if (dto.Plafon <= 0 || dto.Bunga <= 0 || dto.Bunga > 100 || dto.Tenor <= 0)
                throw new ArgumentException("Input tidak valid");

            var kredit = await _context.Kredits.FindAsync(id);
            if (kredit == null) throw new KeyNotFoundException("Data tidak ditemukan");

            kredit.Plafon = dto.Plafon;
            kredit.Bunga = dto.Bunga;
            kredit.Tenor = dto.Tenor;
            kredit.Angsuran = CalculateAngsuran(dto.Plafon, dto.Bunga, dto.Tenor);
            kredit.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new KreditResponseDto
            {
                Id = kredit.Id,
                Plafon = kredit.Plafon,
                Bunga = kredit.Bunga,
                Tenor = kredit.Tenor,
                Angsuran = kredit.Angsuran
            };
        }
    }
}
