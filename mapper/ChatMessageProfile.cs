using AutoMapper;
using Chat.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;
using work_platform_backend.ViewModels;

namespace work_platform_backend.mapper
{
    public class ChatMessageProfile : Profile
    {
        public ChatMessageProfile()
        {

            //Map With ViewModels
            CreateMap<ChatMessage, MessageViewModel>();
    
            CreateMap<MessageViewModel, ChatMessage>();
        }
    }
}
