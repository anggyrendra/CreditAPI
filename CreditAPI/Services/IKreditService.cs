using CreditAPI.DTOs;
using CreditAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreditAPI.Services
{
    public interface IKreditService
    {
        Task<KreditResponseDto> CreateKredit(KreditCreateDto dto);
        Task<KreditResponseDto> GetKreditById(Guid id);
        Task<IEnumerable<KreditResponseDto>> GetAllKredits();
        Task<KreditResponseDto> UpdateKredit(Guid id, KreditUpdateDto dto);
        Task<bool> DeleteKredit(Guid id);
        decimal CalculateAngsuran(decimal plafon, decimal bunga, int tenor);
    }
}
