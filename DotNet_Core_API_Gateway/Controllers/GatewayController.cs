using DotNet_Core_API_Gateway.GatewayInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotNet_Core_API_Gateway.Controllers
{
    [Route("gateway/[controller]")]
    [ApiController]
    public class GatewayController<T>: ControllerBase
    {
        private readonly IGatewayService<T> _gatewayService;
        public GatewayController(IGatewayService<T> _gatewayService)
        {
            this._gatewayService = _gatewayService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _gatewayService.GetAllAsync("", "employees");
            return Ok(result);
        }
    }
}
