using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;
using work_platform_backend.ViewModels;

namespace work_platform_backend.mapper
{
    public class TeamChatProfile : Profile
    {
        public TeamChatProfile()
        {

            //Map With ViewModels
            CreateMap<TeamChat, TeamChatViewModel>();
            CreateMap<TeamChatViewModel, TeamChat>();

        }
      
    }
}
