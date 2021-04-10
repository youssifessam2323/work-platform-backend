using System.Linq;
using AutoMapper;
using work_platform_backend.Dtos;
using work_platform_backend.Models;

namespace work_platform_backend.mapper
{
    public class TeamProfile : Profile
    {
      
        public TeamProfile()
        {
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