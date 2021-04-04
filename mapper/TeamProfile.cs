using AutoMapper;
using work_platform_backend.Models;
using work_platform_backend.ViewModels;

namespace work_platform_backend.mapper
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<Team, TeamViewModel>();
            CreateMap<TeamViewModel, Team>();
        }
    }
}