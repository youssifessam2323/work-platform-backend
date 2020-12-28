using AutoMapper;
using work_platform_backend.Dtos;
using work_platform_backend.Models;

namespace work_platform_backend.mapper
{
    public class UserProfile : Profile
    {

        public UserProfile()
        {

            CreateMap<RegisterDto,User>();
        }


    }
}