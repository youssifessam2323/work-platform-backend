using AutoMapper;
using work_platform_backend.Dtos;
using work_platform_backend.Models;

namespace work_platform_backend.mapper
{
    public class CheckPointProfile : Profile
    {
        public CheckPointProfile()
        {
            CreateMap<CheckPointDto,CheckPoint>().ReverseMap();

            CreateMap<CheckPoint,CheckPointDetailsDto>();
        }
    }
}