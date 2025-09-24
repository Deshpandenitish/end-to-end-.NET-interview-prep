using DotNet_Prep.Caching.Memory.MemoryCacheInterfaces;
using DotNet_Prep.Caching.Memory.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DotNet_Prep.Caching.Memory.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMemoryCacheService(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheService, MemoryCacheService>();
            return services;
        }
    }
}
