﻿using Amphitrite.Dtos;
using Amphitrite.Models;
using AutoMapper;

namespace Amphitrite.Configuration.MapperProfiles
{
    public static class ProfileHelper
    {
        public static void CreatePaginatedMap<TSource, TDestination>(this Profile profile)
        {
            profile.CreateMap<PaginatedElement<TSource>, PaginatedDto<TDestination>>()
                .ForMember(d => d.NextPageUrl, opt =>
                {
                    opt.PreCondition(e => e.PageCount > 1);
                    opt.MapFrom<RestApiResolver, string>(e => opt.DestinationMember.Name);
                })
                .ForMember(d => d.PreviousPageUrl, opt =>
                {
                    opt.PreCondition(e => e.PageCount > 1);
                    opt.MapFrom<RestApiResolver, string>(e => opt.DestinationMember.Name);
                });
        }
    }
}
