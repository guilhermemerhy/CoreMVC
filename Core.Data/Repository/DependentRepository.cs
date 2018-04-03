using Core.Domain.Entities;
using Core.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Data.Repository
{
    public class DependentRepository : IDisposable, IDependentRepository
    {
        protected readonly AppDataContext db;

        public DependentRepository(AppDataContext _db)
        {
            db = _db;
        }


        public async Task Add(Dependent obj)
        {
           await db.Dependents.AddAsync(obj);
        }


        public void RemoveAll(IEnumerable<Dependent> obj)
        {
            db.Dependents.RemoveRange(obj);
        }

        public async Task<IEnumerable<Dependent>> GetAllByEmployee(Guid EmployeeId) =>
            await db.Dependents.Where(w => w.EmployeeId == EmployeeId).ToListAsync();

        public void Dispose()
        {
            db.Dispose();
            GC.SuppressFinalize(this);
        }

      
    }
}
