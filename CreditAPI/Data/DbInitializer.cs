using CreditAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CreditAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.Migrate();

            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    Username = "admin",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin123")
                });
                context.SaveChanges();
            }

            if (!context.Kredits.Any())
            {
                context.Kredits.AddRange(
                    new Kredit { Plafon = 100000000, Bunga = 12, Tenor = 60, Angsuran = 2140000 },
                    new Kredit { Plafon = 50000000, Bunga = 10, Tenor = 36, Angsuran = 1700000 }
                );
                context.SaveChanges();
            }
        }
    }
}
