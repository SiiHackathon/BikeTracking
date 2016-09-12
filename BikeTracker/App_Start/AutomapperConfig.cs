using System;
using AutoMapper;
using BikeTracker.Entities;
using BikeTracker.Models;

namespace BikeTracker
{
    public static class AutomapperConfig
    {
        private const string DefaultTeamImgPath = "~/Images/Teams/Default.png";
        public static void Configure()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Team, TeamViewModel>()
                    .ForMember(d => d.CurrentDistance, m => m.MapFrom(s => (decimal)s.CurrentDistance/1000));
                cfg.CreateMap<Team, TeamStandingsModel>().ForMember(d => d.Image, m => m.MapFrom(s => s.Image ?? DefaultTeamImgPath));
                cfg.CreateMap<Team, TeamEditModel>();
				cfg.CreateMap<Team, TeamDetailsViewModel>().ForMember(d => d.Users, m => m.MapFrom(s => DependencyFactory.CreateUserRepository.GetByTeamId(s.Id)))
				.ForMember(d => d.Image, m => m.MapFrom(s => s.Image ?? DefaultTeamImgPath));

				cfg.CreateMap<User, TeamDetailsUserViewModel>();
				cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<User, UserEditModel>();

                cfg.CreateMap<TeamEditModel, Team>();
                cfg.CreateMap<UserEditModel, User>();
                cfg.CreateMap<AddActivityViewModel, Activity>()
                    .ForMember(d => d.Distance, m => m.MapFrom(s => s.Length*1000))
                    .ForMember(d => d.ActivityDate, m => m.MapFrom(s => s.CreatedOn));
            });
        }
    }
}