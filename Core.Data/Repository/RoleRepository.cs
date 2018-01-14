using Core.Domain.Entities;
using Core.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Repository
{
    public class RoleRepository : IDisposable, IRoleRepository
    {

        protected readonly AppDataContext db;

        public RoleRepository(AppDataContext _db)
        {
            db = _db;
        }



        public IEnumerable<Role> GetAll() => db.Roles.AsNoTracking().ToList();

        public void Dispose()
        {
            db.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
