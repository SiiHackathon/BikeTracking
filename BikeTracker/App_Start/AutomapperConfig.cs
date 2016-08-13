using System;
using AutoMapper;
using BikeTracker.Entities;
using BikeTracker.Models;

namespace BikeTracker.App_Start
{
    public static class AutomapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Team, TeamViewModel>();
            });
        }
    }
}