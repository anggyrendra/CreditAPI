using System;

namespace CreditAPI.DTOs
{
    public class KreditResponseDto
    {
        public Guid Id { get; set; }
        public decimal Plafon { get; set; }
        public decimal Bunga { get; set; }
        public int Tenor { get; set; }
        public decimal Angsuran { get; set; }
    }
}
