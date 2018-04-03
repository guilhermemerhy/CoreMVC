using Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Domain.Repository
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAll();
    }
}
