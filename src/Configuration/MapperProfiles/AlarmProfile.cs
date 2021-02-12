using Amphitrite.Configuration.MapperProfiles;
using Amphitrite.Dtos;
using Amphitrite.Models;
using AutoMapper;

namespace Amphitrite.Configuration.MapperProfiles
{
    public class AlarmProfile : Profile
    {
        public AlarmProfile()
        {
            CreateMap<Alarm, AlarmDto>()
                .ForMember(d => d.IsAck, opt => opt.MapFrom(s => s.Ack))
                .ForMember(d => d.OccuredAt, opt => opt.MapFrom(s => s.DateTime))
                .ForMember(d => d.AlarmType, opt => opt.MapFrom(s => s.AlarmType.ToString()))
                .ForMember(d => d.AcknowledgmentUri, opt => opt.MapFrom<RestApiResolver>());

            this.CreatePaginatedMap<Alarm, AlarmDto>();
        }
    }
}
