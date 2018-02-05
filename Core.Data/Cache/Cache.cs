using Core.Domain.Cache;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Data.Cache
{
    public class Cache : ICache
    {
        private IMemoryCache _cache;

        public Cache(IMemoryCache cache)
        {
            _cache = cache;
        }


        public bool GetKey(string key, out object value) => _cache.TryGetValue(key, out value);

        public void SetKey(string key, object value, DateTime? timeExpire)
        {
            var opcoesDoCache = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = timeExpire,
            };
           
            _cache.Set(key, value, opcoesDoCache);
        }
    }
}
