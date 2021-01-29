using AutoMapper;
using work_platform_backend.Dtos;
using work_platform_backend.Models;

namespace work_platform_backend.mapper
{
    public class UserProfile : Profile
    {

        public UserProfile()
        {
            //Source=>Target

            CreateMap<RegisterRequest,User>();
            CreateMap<User,RegisterRequest>();

            CreateMap<RequestRoomDto, Room>();
            CreateMap<Room, ResponseRoomDto>();
            CreateMap<ResponseRoomDto, Room>();

            CreateMap<Team, ResponseTeamDto>();

            CreateMap<Project, ResponseProjectDto>();
            CreateMap<ProjectManager, ResponseProjectManagersDto>();

            CreateMap<RTask,ResponseTeamRTaskDto >();
            CreateMap<RTask, ResponseProjectTasksDto>();
            CreateMap<User, ResponseRoomCreatorDto>();
        }


    }
}