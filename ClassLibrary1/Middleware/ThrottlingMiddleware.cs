using DotNet_Prep.Throttling.Interface;
using Microsoft.AspNetCore.Http;

namespace DotNet_Prep.Throttling.Middleware
{
    public class ThrottlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IThrottleService _throttleService;
        private const int limit = 5;
        public ThrottlingMiddleware(RequestDelegate next, IThrottleService throttleService)
        {
            _next = next;
            _throttleService = throttleService;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            var clientKey = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            if (!_throttleService.IsRequestAllowed(clientKey, limit))
            {
                httpContext.Response.StatusCode = 429;
                await httpContext.Response.WriteAsync("Too many requests. Try again later");
                return;
            }
            await _next(httpContext);
        }
    }
}
