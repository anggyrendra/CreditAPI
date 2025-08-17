using CreditAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CreditAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Kredit> Kredits { get; set; }
        public DbSet<Angsuran> Angsurans { get; set; }
        public DbSet<PengajuanKredit> PengajuanKredits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kredit>()
                .HasIndex(k => k.Plafon);
            modelBuilder.Entity<Kredit>()
                .HasIndex(k => k.Tenor);
        }
    }
}
