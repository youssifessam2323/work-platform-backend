using AutoMapper;
using work_platform_backend.Dtos;
using work_platform_backend.Models;

namespace work_platform_backend.mapper
{
    public class AttachmentProfile : Profile
    {
        public AttachmentProfile()
        {
            CreateMap<AttachmentDto,Attachment>().ReverseMap();
            CreateMap<Attachment,AttachmentDetailsDto>();
        }
        
    }
}