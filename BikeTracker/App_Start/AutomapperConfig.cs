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
                cfg.CreateMap<Team, TeamStandingsModel>();
                cfg.CreateMap<Team, TeamEditModel>();
                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<User, UserEditModel>();

                cfg.CreateMap<TeamEditModel, Team>();
                cfg.CreateMap<UserEditModel, User>();
                cfg.CreateMap<AddActivityViewModel, Activity>()
                    .ForMember(d => d.Distance, m => m.MapFrom(s => s.Length*1000));
            });
        }
    }
}