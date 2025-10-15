using DotNet_Prep.Throttling.Interface;
using Microsoft.Extensions.Caching.Memory;

namespace DotNet_Prep.Throttling.Service
{
    public class MemoryThrottleService: IThrottleService
    {
        private readonly IMemoryCache _cache;
        public MemoryThrottleService(IMemoryCache cache) => _cache = cache;

        public bool IsRequestAllowed(string clientKey, int limit)
        {
            var key = $"throttle_{clientKey}";
            if (_cache.TryGetValue(key, out int count))
            {
                if (count >= limit) return false;
                else
                {
                    _cache.Set(key, ++count);
                    return true;
                }
            }
            else
            {
                _cache.Set(key, 1, TimeSpan.FromMinutes(1));
                return true;
            }
        }
    }
}
