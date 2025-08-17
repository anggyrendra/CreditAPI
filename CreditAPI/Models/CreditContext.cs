using Microsoft.EntityFrameworkCore;

namespace CreditAPI.Models
{
    public class CreditContext : DbContext
    {
        public CreditContext(DbContextOptions<CreditContext> options)
            : base(options) { }

        public DbSet<Kredit> Kredits { get; set; }
        public DbSet<Angsuran> Angsurans { get; set; }
    }
}
