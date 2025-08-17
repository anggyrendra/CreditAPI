using System;

namespace CreditAPI.Models
{
    public class Kredit
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Plafon { get; set; }
        public decimal Bunga { get; set; } 
        public int Tenor { get; set; } 
        public decimal Angsuran { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
      public ICollection<Angsuran> Angsurans { get; set; } = new List<Angsuran>();
    }
}
