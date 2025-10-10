using DotNet_Core_Prep_Samples.Interfaces;
using DotNet_Core_Prep_Samples.Repositories;

namespace DotNet_Core_Prep_Samples.Configs
{
    public static class InjectService
    {
        public static void InjectServices(IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }
    }
}
