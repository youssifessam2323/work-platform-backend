using AutoMapper;
using work_platform_backend.Models;

namespace work_platform_backend.Dtos
{
    public class CommentProfile : Profile
    {
        
        public CommentProfile()
        {
            CreateMap<CommentDto,Comment>().ReverseMap();

            CreateMap<Comment,CommentDetailsDto>().ReverseMap();
        }
        

    }
}