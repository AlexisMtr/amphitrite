using Amphitrite.Dtos;
using Amphitrite.Models;
using AutoMapper;

namespace Amphitrite.Configuration.MapperProfiles
{
    public class DeviceProfile : Profile
    {
        public DeviceProfile()
        {
            CreateMap<DeviceConfigurationDto, DeviceConfiguration>()
                .ForMember(d => d.IsPublished, opt => opt.MapFrom(c => false));
            CreateMap<Device, string>()
                .ConvertUsing(e => e.DeviceId);

            CreateMap<DeviceConfiguration, DeviceConfigurationDto>();
        }
    }
}
