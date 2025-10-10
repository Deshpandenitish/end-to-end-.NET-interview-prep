using DotNet_Core_Prep_Samples.Configs;
using DotNet_Core_Prep_Samples.Data;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Core_Prep_Samples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddDbContext<TrialsContext>(x => x.UseSqlServer
            (builder.Configuration.GetConnectionString("DefaultConnection")));

            InjectService.InjectServices(builder.Services);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddApiVersioning(option =>
            {
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            //Health checkup before Gateway hits
            app.MapGet("/health", () => Results.Ok("Healthy"));
            app.Run();
        }
    }
}
