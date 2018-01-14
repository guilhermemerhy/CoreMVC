using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Data.Data
{
    public static class DbInitializer
    {
        public static void Initialize()
        {
            var context = new AppDataContext();
            context.Database.EnsureCreated();


        }
    }
}
