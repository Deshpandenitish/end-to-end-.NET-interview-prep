using DotNet_Core_API_Gateway.Helpers.Routes;

namespace DotNet_Core_API_Gateway.Helpers.Configs
{
    public class EmployeeServiceConfig
    {
        public string BaseUrl { get; set; } = string.Empty;
        public EmployeeRoutes Routes { get; set; } = new();
    }
}
