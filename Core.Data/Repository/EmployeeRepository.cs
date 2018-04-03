using Core.Domain.Entities;
using Core.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Data.Repository
{
    public class EmployeeRepository : IDisposable, IEmployeeRepository
    {
        protected readonly AppDataContext db;

        public EmployeeRepository(AppDataContext _db)
        {
            db = _db;
        }

        public async Task Add(Employee obj)
        {
           await db.Employees.AddAsync(obj);
        }

        public async Task<IEnumerable<Employee>> GetAll() => await db.Employees.AsNoTracking().Include(x => x.Role).Include(x => x.Dependent).ToListAsync();

        public async Task<Employee> GetById(Guid? id) => await db.Employees.Include(x => x.Role).Include(x => x.Dependent).FirstOrDefaultAsync(f => f.Id == id);

        public void Remove(Employee obj)
        {
             db.Employees.Remove(obj);
        }

        public void Update(Employee obj)
        {
            db.Employees.Update(obj);
        }

        public void Dispose()
        {
            db.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<bool> GetByEmail(string email, Guid? id) => await db.Employees.AnyAsync(x => x.Id != id && x.Email.Address == email);
    }
}
