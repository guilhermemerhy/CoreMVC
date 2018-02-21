using Core.Domain.Entities;
using System.Linq;

namespace Core.Data.Data
{
    public static class DbInitializer
    {
        public static void Initialize()
        {
            var context = new AppDataContext();
            context.Database.EnsureCreated();

            if (context.Roles.Any())
            {
                return;   // Já contém dados
            }

            var role = new Role[]
            {                       
               new Role("Analista de negócio"),
               new Role("Analista de sistemas"),
               new Role("Gerente de projetos"),
               new Role("Diretor de TI"),
               new Role("Recursos Humanos")
            };

            context.Roles.AddRange(role);
            context.SaveChanges();
        }
    }
}
