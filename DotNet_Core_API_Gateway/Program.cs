using DotNet_Core_API_Gateway.GatewayInterfaces;
using DotNet_Core_API_Gateway.Helpers;
using DotNet_Core_API_Gateway.Helpers.Configs;
using DotNet_Core_API_Gateway.Services;
using DotNet_Prep.Caching.Memory.Extensions;
using DotNet_Prep.Throttling.Extensions;
using DotNet_Prep.Throttling.Service;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace DotNet_Core_API_Gateway
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
            // Register Ocelot
            builder.Services.AddOcelot(builder.Configuration);

            //Throttle / Rate Limiting
            builder.Services.AddMemoryThrottle();
            builder.Services.Configure<ThrottleSettings>(builder.Configuration.GetSection("ThrottleSettings"));

            //Register Memory Cache
            builder.Services.AddMemoryCacheService();

            builder.Services.AddHttpClient();
            builder.Services.AddSingleton(typeof(IGatewayService<>), typeof(GatewayService<>));
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Add Routes for Employees API Services
            builder.Services.Configure<EmployeeServiceConfig>(builder.Configuration.GetSection("ApiEndPoints:EmployeeService"));

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddAuthentication();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseThrottling();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            //Wait for Downstream service to be ready.
            var apiHost = builder.Configuration["ApiServiceHost"];
            if (!string.IsNullOrEmpty(apiHost))
                HealthChecks.WaitForDownStreamApiService(apiHost);

            await app.UseOcelot();
            app.Run();
        }
    }
}
