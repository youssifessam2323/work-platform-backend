using System.Linq;
using AutoMapper;
using work_platform_backend.Dtos;
using work_platform_backend.Models;

namespace work_platform_backend.mapper
{
    public class TaskMapper : Profile
    {

        public TaskMapper()
        {
            CreateMap<RTask,TaskDetailsDto>()
            .ForMember(dest => dest.AssignedUsers
            ,x => x.MapFrom(src => src.UserTasks.Select(ut => ut.User)));

            CreateMap<TaskDetailsDto,RTask>();
        
            CreateMap<RTask,TaskDto>()
            .ForMember(dest => dest.AssignedUsers
            ,x => x.MapFrom(src => src.UserTasks.Select(ut => ut.User))).ReverseMap();
            
        }
    }
}