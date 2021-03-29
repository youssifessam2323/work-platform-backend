using AutoMapper;
using work_platform_backend.Dtos;
using work_platform_backend.Dtos.Response;
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

            CreateMap<RoomRequest, Room>();
            CreateMap<Room, ResponseRoomDto>();
            CreateMap<ResponseRoomDto, Room>();

            CreateMap<Team, ResponseTeamDto>();

            CreateMap<Project, ResponseProjectDto>();
            CreateMap<ProjectManager, ResponseProjectManagersDto>();

            CreateMap<RTask,ResponseTeamRTaskDto >();
            CreateMap<RTask, ResponseProjectTasksDto>();
            CreateMap<User, ResponseRoomCreatorDto>();


            CreateMap<UserResponse,User>();
            CreateMap<User,UserResponse>();

            //Map With ViewModels
            CreateMap<User, UserViewModel>()
            .ForMember(dst => dst.Username, opt => opt.MapFrom(x => x.UserName));
            CreateMap<UserViewModel, User>();
        }


    }
}