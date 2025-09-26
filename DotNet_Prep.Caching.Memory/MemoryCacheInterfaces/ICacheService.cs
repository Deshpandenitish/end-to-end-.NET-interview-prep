using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_Prep.Caching.Memory.MemoryCacheInterfaces
{
    public interface ICacheService
    {
        T Get<T>(string key);
        void Set<T>(string Key, T value, TimeSpan? absoluteExpiration = null);
        void Remove(string Key);
        public bool TryGetValue<T>(string key, out T? value);
    }
}
