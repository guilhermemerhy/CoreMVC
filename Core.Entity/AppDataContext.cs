using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity
{
    public class AppDataContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Dependent> Dependents { get; set; }

        //public AppDataContext(DbContextOptions<AppDataContext> opcoes)
        //  { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new DependentMap());



            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            // define the database to use
            //Server = (localdb)\\mssqllocaldb; Database = IMusica;user id=developer;password=123456;  Trusted_Connection = True; MultipleActiveResultSets = true");
            optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = IMusica;user id=developer;password=123456;  Trusted_Connection = True; MultipleActiveResultSets = true");
        }
    }
}
