using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using DotNet_Prep.Caching.Memory.Extensions;
using DotNet_Core_API_Gateway.GatewayInterfaces;
using DotNet_Prep.Caching.Memory.MemoryCacheInterfaces;

namespace DotNet_Core_API_Gateway.Services
{
    public class GatewayService<T>: IGatewayService<T> where T : class
    {
        private readonly HttpClient _httpClient;
        private readonly ICacheService _cacheService;
        public GatewayService(HttpClient httpClient, ICacheService cacheService)
        {
            _httpClient = httpClient;
            _cacheService = cacheService;
        }
        public async Task<List<T>> GetAllAsync(string endPoint, string cacheKey, TimeSpan? cacheDuration)
        {
            var cached = _cacheService.Get<List<T>>(cacheKey);
            if (cached != null) { return cached; }
            else
            {
                var response = await _httpClient.GetAsync(endPoint);
                if (!response.IsSuccessStatusCode) return new List<T>();
                else
                {
                    var result = await response.Content.ReadFromJsonAsync<List<T>>();
                    if (result != null)
                        _cacheService.Set(cacheKey, result, cacheDuration ?? TimeSpan.FromMinutes(5));
                    return result ?? new List<T>();
                }
            }
        }
    }
}
