using EFEntities;
using Microsoft.EntityFrameworkCore;
using System;

namespace EFData
{
    public class EFDataContext : DbContext
    {
        public DbSet<AppCat> Cats { get; set; }
        public DbSet<AppCatPrice> CatPrices { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=91.238.103.51;Port=5743;Database=denysdb;Username=denys;Password=qwerty1*;");
        }
    }
}
