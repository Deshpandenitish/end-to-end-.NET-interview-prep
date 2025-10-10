using DotNet_Core_API_Gateway.Controllers.Base;
using DotNet_Core_API_Gateway.GatewayInterfaces;
using DotNet_Core_API_Gateway.Helpers.Configs;
using DotNet_Core_API_Gateway.Helpers.Mappers.DTO;
using Microsoft.Extensions.Options;

namespace DotNet_Core_API_Gateway.Controllers
{
    public class EmployeeGatewayController: GatewayBaseController<EmployeeDTO>
    {
        public EmployeeGatewayController(IGatewayService<EmployeeDTO> services, IOptions<EmployeeServiceConfig> options) : base(services, options) { }
    }
}
