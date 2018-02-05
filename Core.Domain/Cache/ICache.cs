using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Cache
{
    public interface ICache
    {
        bool GetKey(string key, out object value);
        void SetKey(string key, object value, DateTime? timeExpire);
    }
}
