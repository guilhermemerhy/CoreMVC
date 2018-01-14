using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Repository
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAll();
    }
}
