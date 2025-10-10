using DotNet_Core_API_Gateway.GatewayInterfaces;
using Microsoft.AspNetCore.Mvc;
using DotNet_Core_API_Gateway.Helpers.Configs;
using Microsoft.Extensions.Options;

namespace DotNet_Core_API_Gateway.Controllers.Base
{
    [Route("gateway/[controller]")]
    [ApiController]
    public class GatewayBaseController<T>: ControllerBase where T : class
    {
        private readonly IGatewayService<T> _gatewayService;
        private readonly EmployeeServiceConfig _config;
        public GatewayBaseController(IGatewayService<T> _gatewayService, IOptions<EmployeeServiceConfig> options)
        {
            this._gatewayService = _gatewayService;
            _config = options.Value;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var url = $"{_config.BaseUrl}{_config.Routes.GetAllEmployees}";
            var result = await _gatewayService.GetAllAsync(url, "employees");
            return Ok(result);
        }
    }
}
