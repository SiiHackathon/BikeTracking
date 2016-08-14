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
                cfg.CreateMap<AddDistanceViewModel, Distance>()
                .ForMember(d => d.Length, m => m.MapFrom(s => s.Length*1000));
            });
        }
    }
}