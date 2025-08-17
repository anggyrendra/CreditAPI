using System;
using System.Collections.Generic;

namespace CreditAPI.Models
{
    public class PengajuanKredit
    {
        public Guid Id { get; set; }
        public decimal Plafon { get; set; }
        public decimal Bunga { get; set; }
        public int Tenor { get; set; }
        public decimal AngsuranPerBulan { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Angsuran> Angsurans { get; set; }
    }
}
