using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreditAPI.Models
{
    public class Angsuran
    {
        [Key]
        public Guid Id { get; set; }

        public Guid KreditId { get; set; }

        [Required]
        public decimal Jumlah { get; set; } 

        [Required]
        public decimal SudahDibayar { get; set; }

        [Required]
        public DateTime JatuhTempo { get; set; }  

        [Required]
        public bool Lunas { get; set; } = false;   


        public Guid PengajuanId { get; set; }  
        public decimal JumlahAngsuran { get; set; }  
        public int AngsuranKe { get; set; }  

        public decimal? JumlahBayar { get; set; }
        public DateTime? TanggalBayar { get; set; }
        public bool StatusLunas { get; set; }

	
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
       
        public PengajuanKredit Pengajuan { get; set; } = null!;
        public Kredit Kredit { get; set; } 


    }
}
