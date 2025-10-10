using AutoMapper;
using DotNet_Core_API_Gateway.Helpers.Mappers.DTO;
using DotNet_Core_API_Gateway.Helpers.Mappers.RawEntities;

namespace DotNet_Core_API_Gateway.Helpers.Mappers
{
    public class GatewayMappingProfile: Profile
    {
        public GatewayMappingProfile()
        {
            CreateMap<EmployeeRaw, EmployeeDTO>();
        }
    }
}
