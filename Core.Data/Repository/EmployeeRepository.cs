using Core.Domain.Entities;
using Core.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Repository
{
    public class EmployeeRepository : IDisposable, IEmployeeRepository
    {
        protected readonly AppDataContext db;

        public EmployeeRepository(AppDataContext _db)
        {
            db = _db;
        }

        public void Add(Employee obj)
        {
            db.Employees.Add(obj);
        }

        public IEnumerable<Employee> GetAll() => db.Employees.AsNoTracking().Include(x => x.Role).Include(x => x.Dependent).ToList();

        public Employee GetById(Guid? id) => db.Employees.Include(x => x.Role).Include(x => x.Dependent).FirstOrDefault(f => f.Id == id);

        public void Remove(Employee obj)
        {
            db.Employees.Remove(obj);
        }

        public void Update(Employee obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        public void Dispose()
        {
            db.Dispose();
            GC.SuppressFinalize(this);
        }

        public bool GetByEmail(string email, Guid? id) => db.Employees.Any(x => x.Id != id && x.Email.Address == email);
    }
}
