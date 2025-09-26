using DotNet_Prep.Caching.Memory.MemoryCacheInterfaces;
using Microsoft.Extensions.Caching.Memory;

namespace DotNet_Prep.Caching.Memory.Services
{
    public class MemoryCacheService: ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public T? Get<T>(string key)
        {
            return _memoryCache.TryGetValue(key, out T value) ? value : default;
        }
        public void Set<T>(string key, T value, TimeSpan? expiry = null)
        {
            var options = new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiry };
            _memoryCache.Set(key, value, options);
        }
        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
        public bool TryGetValue<T>(string key, out T? value)
        {
            return _memoryCache.TryGetValue(key, out value);
        }
    }
}
