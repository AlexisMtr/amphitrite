using Amphitrite.Dtos;
using Amphitrite.Helpers;
using Amphitrite.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Amphitrite.Configuration.MapperProfiles
{
    public class RestApiResolver :
        IValueResolver<Alarm, AlarmDto, string>,
        IMemberValueResolver<IPaginatedResource, IPaginatedDto, string, string>
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public RestApiResolver(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string Resolve(Alarm source, AlarmDto destination, string destMember, ResolutionContext context)
        {
            return source.Ack ? null : httpContextAccessor.HttpContext.GetAcknowledgmentAlarmUri(source.Id);
        }

        public string Resolve(IPaginatedResource source, IPaginatedDto destination, string sourceMember, string destMember, ResolutionContext context)
        {
            string pageNumber = httpContextAccessor.HttpContext.Request.Query
                .FirstOrDefault(e => string.Compare(e.Key, "pageNumber", true) == 0).Value;
            if (string.IsNullOrEmpty(pageNumber)) pageNumber = "1";

            string url = null;

            if (sourceMember.Equals(nameof(IPaginatedDto.NextPageUrl)))
                url = httpContextAccessor.HttpContext.GetNextPageUrl(int.Parse(pageNumber) < source.PageCount);
            else if (sourceMember.Equals(nameof(IPaginatedDto.PreviousPageUrl)))
                url = httpContextAccessor.HttpContext.GetPreviousPageUrl(int.Parse(pageNumber) > 1);

            return url;
        }
    }
}
