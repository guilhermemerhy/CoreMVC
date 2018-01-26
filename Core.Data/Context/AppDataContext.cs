using Core.Data.Mapping;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Data
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
            // .\\SQLEXPRESS
            optionsBuilder.UseSqlServer("server=(LocalDb)\\MSSQLLocalDB;initial catalog=Core;Integrated Security=False;User Id=developer;Password=123456;Connection Timeout=360;MultipleActiveResultSets=True;Application Name=Core;Pooling=false;Max Pool Size=500;Min Pool Size=20;Connection lifetime=300");
        }
    }
}
