using Core.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Core.Domain.Repository
{
    public interface IEmployeeRepository
    {
        void Add(Employee obj);
        Employee GetById(Guid? id);
        bool GetByEmail(string email, Guid? id);
        IEnumerable<Employee> GetAll();
        void Update(Employee obj);
        void Remove(Employee obj);
    }
}
