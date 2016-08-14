using System;
using AutoMapper;
using BikeTracker.Entities;
using BikeTracker.Models;

namespace BikeTracker
{
    public static class AutomapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Team, TeamViewModel>();
                cfg.CreateMap<AddActivityViewModel, Activity>()
                .ForMember(d => d.Distance, m => m.MapFrom(s => s.Length*1000));
            });
        }
    }
}