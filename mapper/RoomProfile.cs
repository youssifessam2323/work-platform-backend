using System.Linq;
using AutoMapper;
using work_platform_backend.Dtos;
using work_platform_backend.Models;

namespace work_platform_backend.mapper
{
    public class RoomProfile : Profile
    {
       
        public RoomProfile()
       {
           CreateMap<Room,RoomDetailsDto>()
            .ForMember(dest => dest.ProjectManagers
            , x => x.MapFrom(src => src.ProjectManagers.Select(pm => pm.User)));

           CreateMap<Room,RoomDto>().ReverseMap();

       }
    }
}