using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public class SamuraiContext : DbContext
    {
        public DbSet<Samurai> Samurais { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            {
                if (optionsBuilder.IsConfigured == false)
                {
                    optionsBuilder.UseSqlServer(
                   @"Data Source=(localdb)\\mssqllocaldb;Initial Catalog=EFCoreFullNet;
                       Integrated Security=True;");
                }
                base.OnConfiguring(optionsBuilder);
            }
        }
        public SamuraiContext(DbContextOptions<SamuraiContext> options)
          : base(options) { }
    }
}
