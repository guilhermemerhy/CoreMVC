using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class Samurai
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
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
