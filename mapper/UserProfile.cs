using AutoMapper;
using work_platform_backend.Dtos;
using work_platform_backend.Models;
using work_platform_backend.ViewModels;

namespace work_platform_backend.mapper
{
    public class UserProfile : Profile
    {

        
        public UserProfile()
        {
            //Source=>Target

            CreateMap<RegisterRequest,User>();
            CreateMap<User,RegisterRequest>();

            CreateMap<User,UserDetailsDto>();
            CreateMap<User,UserDto>().ReverseMap();



            //Map With ViewModels
            CreateMap<User, UserViewModel>()
            .ForMember(dst => dst.Username, opt => opt.MapFrom(x => x.UserName));
            CreateMap<UserViewModel, User>();
        }


    }
}