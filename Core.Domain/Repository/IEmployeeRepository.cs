using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Domain.Repository
{
    public interface IEmployeeRepository
    {
        Task Add(Employee obj);
        Task<Employee> GetById(Guid? id);
        Task<bool> GetByEmail(string email, Guid? id);
        Task<IEnumerable<Employee>> GetAll();
        void Update(Employee obj);
        void Remove(Employee obj);
    }
}
