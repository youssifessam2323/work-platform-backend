using AutoMapper;
using work_platform_backend.Models;
using work_platform_backend.ViewModels;
using System.Linq;
using work_platform_backend.Dtos;

namespace work_platform_backend.mapper
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<Team, TeamViewModel>();
            CreateMap<TeamViewModel, Team>();
      
            CreateMap<Team,TeamDetailsDto>()
            .ForMember(dest => dest.TeamMembers
            , x => x.MapFrom(src => src.TeamMembers.Select(tm => tm.User)
            ))
            .ForMember(dest => dest.TeamProjects
            , x => x.MapFrom(src => src.TeamProjects.Select(tm => tm.Project)));
            

            CreateMap<Team,TeamDto>().ReverseMap();
        }
    }
}