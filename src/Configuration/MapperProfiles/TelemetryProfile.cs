using Amphitrite.Dtos;
using Amphitrite.Models;
using AutoMapper;

namespace Amphitrite.Configuration.MapperProfiles
{
    public class TelemetryProfile : Profile
    {
        public TelemetryProfile()
        {
            CreateMap<Telemetry, TelemetryDto>()
                .ForMember(d => d.DateTime, opt => opt.MapFrom(s => s.DateTime.DateTime));
            CreateMap<PaginatedElement<Telemetry>, PaginatedDto<TelemetryDto>>();
        }
    }
}
