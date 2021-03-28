using AutoMapper;
using work_platform_backend.Dtos.Response;
using work_platform_backend.Models;

namespace work_platform_backend.mapper
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<Team,TeamResponse>();
            CreateMap<TeamResponse,Team>();
        }
    }
}