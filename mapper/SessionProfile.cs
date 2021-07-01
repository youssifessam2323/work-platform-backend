using AutoMapper;
using work_platform_backend.Dtos;
using work_platform_backend.Models;

namespace work_platform_backend.mapper
{
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            CreateMap<Session,SessionDto>().ReverseMap();

            CreateMap<Session,CurrentSessionDto>().ReverseMap();
        }
    }
}