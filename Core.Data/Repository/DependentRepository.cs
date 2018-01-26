using Core.Domain.Entities;
using Core.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Data.Repository
{
    public class DependentRepository : IDisposable, IDependentRepository
    {
        protected readonly AppDataContext db;

        public DependentRepository(AppDataContext _db)
        {
            db = _db;
        }


        public void Add(Dependent obj)
        {
            db.Dependents.Add(obj);
        }


        public void Remove(Dependent obj)
        {
            db.Dependents.Remove(obj);
        }

        public IEnumerable<Dependent> GetAllByEmployee(Guid EmployeeId) => db.Dependents.Where(w => w.EmployeeId == EmployeeId).ToList();

        public void Dispose()
        {
            db.Dispose();
            GC.SuppressFinalize(this);
        }

       
    }
}
