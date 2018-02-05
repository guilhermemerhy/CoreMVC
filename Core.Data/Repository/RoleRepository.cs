using Core.Domain.Cache;
using Core.Domain.Entities;
using Core.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Data.Repository
{
    public class RoleRepository : IDisposable, IRoleRepository
    {

        protected readonly AppDataContext db;
        private ICache _cache;

        public RoleRepository(AppDataContext _db, ICache cache)
        {
             db = _db;
            _cache = cache;
        }


        public IEnumerable<Role> GetAll()
        {
            var chaveDoCache = "role";
            dynamic role;

            if (!_cache.GetKey(chaveDoCache, out role))
            {               
                 role = db.Roles.AsNoTracking().ToList();

                _cache.SetKey(chaveDoCache, role, DateTime.Now.AddHours(1));
            }

            return (IEnumerable<Role>)role;

        }

        public void Dispose()
        {
            db.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
