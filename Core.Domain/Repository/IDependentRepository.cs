using Core.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Core.Domain.Repository
{
    public interface IDependentRepository
    {
        void Add(Dependent obj);
        void Remove(Dependent obj);
        void RemoveAll(IEnumerable<Dependent> obj);
        IEnumerable<Dependent> GetAllByEmployee(Guid EmployeeId);
    }
}
