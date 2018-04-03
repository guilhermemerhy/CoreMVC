using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Domain.Repository
{
    public interface IDependentRepository
    {
        Task Add(Dependent obj);        
        void RemoveAll(IEnumerable<Dependent> obj);
        Task<IEnumerable<Dependent>> GetAllByEmployee(Guid EmployeeId);
    }
}
