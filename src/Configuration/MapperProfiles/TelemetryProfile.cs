using Amphitrite.Dtos;
using Amphitrite.Models;
using AutoMapper;

namespace Amphitrite.Configuration.MapperProfiles
{
    public class TelemetryProfile : Profile
    {
        public TelemetryProfile()
        {
            CreateMap<Telemetry, TelemetryDto>();
            CreateMap<PaginatedElement<Telemetry>, PaginatedDto<TelemetryDto>>();
        }
    }
}
