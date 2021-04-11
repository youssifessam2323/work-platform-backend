using System.Linq;
using AutoMapper;
using work_platform_backend.Dtos;
using work_platform_backend.Models;

namespace work_platform_backend.mapper
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project,ProjectDetailsDto>()
            .ForMember(dest => dest.TeamProjects,
            x => x.MapFrom(src => src.TeamProjects.Select(tp => tp.Team)));
            
            CreateMap<Project,ProjectDto>().ReverseMap();
        }
    }
}