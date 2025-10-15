using DotNet_Prep.Throttling.Interface;
using DotNet_Prep.Throttling.Middleware;
using DotNet_Prep.Throttling.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DotNet_Prep.Throttling.Extensions
{
    public static class ThrottlingExtensions
    {
        public static IServiceCollection AddMemoryThrottle(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<IThrottleService, MemoryThrottleService>();
            return services;
        }
        public static IApplicationBuilder UseThrottling(this IApplicationBuilder app)
        {
            app.UseMiddleware<ThrottlingMiddleware>();
            return app;
        }
    }
}
