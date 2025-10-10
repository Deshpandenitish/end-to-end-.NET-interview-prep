using DotNet_Core_API_Gateway.GatewayInterfaces;
using DotNet_Prep.Caching.Memory.MemoryCacheInterfaces;
using System.Text.Json;

namespace DotNet_Core_API_Gateway.Services
{
    public class GatewayService<T>: IGatewayService<T> where T : class
    {
        private readonly HttpClient _httpClient;
        private readonly ICacheService _cacheService;
        public GatewayService(IHttpClientFactory httpClientFactory, ICacheService cacheService)
        {
            _httpClient = httpClientFactory.CreateClient();
            _cacheService = cacheService;
        }
        public async Task<IEnumerable<T>> GetAllAsync(string endPoint, string cacheKey, TimeSpan? cacheDuration)
        {
            var cached = _cacheService.Get<List<T>>(cacheKey);
            if (cached != null) { return cached; }
            else
            {
                var response = await _httpClient.GetAsync(endPoint);
                if (!response.IsSuccessStatusCode) return new List<T>();
                else
                {
                    var rawData = JsonSerializer.Deserialize<IEnumerable<T>>(
                                await response.Content.ReadAsStringAsync(),
                                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (rawData is not null)
                        _cacheService.Set(cacheKey, rawData, cacheDuration ?? TimeSpan.FromMinutes(5));
                    return rawData ?? new List<T>();
                }
            }
        }
    }
}
