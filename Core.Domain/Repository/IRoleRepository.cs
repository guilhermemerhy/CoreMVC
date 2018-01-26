using Core.Domain.Entities;
using System.Collections.Generic;

namespace Core.Domain.Repository
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAll();
    }
}
