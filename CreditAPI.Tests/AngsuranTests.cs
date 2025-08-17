using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;
using CreditAPI.Models;  
using CreditAPI.Data;    


namespace CreditAPI.Tests
{
    public class AngsuranTests
    {
        private CreditContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<CreditContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
                .Options;

            return new CreditContext(options);
        }

        [Fact]
        public void Can_Create_Angsuran()
        {
            using var context = GetInMemoryDbContext();

            var angsuran = new Angsuran
            {
                Id = Guid.NewGuid(),
                KreditId = Guid.NewGuid(),
                TanggalBayar = DateTime.Now,
                JumlahBayar = 500000
            };

            context.Angsurans.Add(angsuran);
            context.SaveChanges();

            var saved = context.Angsurans.FirstOrDefault(a => a.Id == angsuran.Id);
            Assert.NotNull(saved);
            Assert.Equal(500000, saved.JumlahBayar);
        }

        [Fact]
        public void Can_Read_Angsuran()
        {
            using var context = GetInMemoryDbContext();

            var angsuran = new Angsuran
            {
                Id = Guid.NewGuid(),
                KreditId = Guid.NewGuid(),
                TanggalBayar = DateTime.Now,
                JumlahBayar = 750000
            };

            context.Angsurans.Add(angsuran);
            context.SaveChanges();

            var fetched = context.Angsurans.Find(angsuran.Id);
            Assert.NotNull(fetched);
            Assert.Equal(750000, fetched.JumlahBayar);
        }

        [Fact]
        public void Can_Update_Angsuran()
        {
            using var context = GetInMemoryDbContext();

            var angsuran = new Angsuran
            {
                Id = Guid.NewGuid(),
                KreditId = Guid.NewGuid(),
                TanggalBayar = DateTime.Now,
                JumlahBayar = 1000000
            };

            context.Angsurans.Add(angsuran);
            context.SaveChanges();

            // Update
            angsuran.JumlahBayar = 1500000;
            context.Angsurans.Update(angsuran);
            context.SaveChanges();

            var updated = context.Angsurans.Find(angsuran.Id);
            Assert.NotNull(updated);
            Assert.Equal(1500000, updated.JumlahBayar);
        }

        [Fact]
        public void Can_Delete_Angsuran()
        {
            using var context = GetInMemoryDbContext();

            var angsuran = new Angsuran
            {
                Id = Guid.NewGuid(),
                KreditId = Guid.NewGuid(),
                TanggalBayar = DateTime.Now,
                JumlahBayar = 2000000
            };

            context.Angsurans.Add(angsuran);
            context.SaveChanges();

            // Delete
            context.Angsurans.Remove(angsuran);
            context.SaveChanges();

            var deleted = context.Angsurans.Find(angsuran.Id);
            Assert.Null(deleted);
        }
    }
}
