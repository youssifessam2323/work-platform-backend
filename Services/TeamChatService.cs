using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class TeamChatService
    {
        private readonly IMapper mapper;
        private readonly ITeamChatRepository teamChatRepository;

        public TeamChatService(IMapper mapper, ITeamChatRepository teamChatRepository)
        {
            this.mapper = mapper;
            this.teamChatRepository = teamChatRepository;
        }


        public async Task<TeamChat> CreateTeamChat(TeamChat newTeamChat, string CreatorId, int TeamId)
        {

            if (newTeamChat != null)
            {

                newTeamChat.CreatorId = CreatorId;
                newTeamChat.TeamId = TeamId;
                await teamChatRepository.SaveTeamChat(newTeamChat);
                await teamChatRepository.SaveChanges();
                return newTeamChat;
            }
            return null;

        }

        public async Task DeleteTeamChat(int teamChatId)
        {
            var teamChat = await teamChatRepository.DeleteTeamChatById(teamChatId);
            if (teamChat == null)
            {

                throw new NullReferenceException();

            }

            await teamChatRepository.SaveChanges();


        }



        public async Task<TeamChat> GetTeamChat(int teamChatId)
        {
            var teamChat = await teamChatRepository.GetTeamChatById(teamChatId);

            if (teamChat == null)
            {
                return null;

            }

            return teamChat;

        }


        public async Task<TeamChat> GetTeamChatOfTeam(int teamId)
        {
            var teamChat = await teamChatRepository.GetTeamChatByTeam(teamId);

            if (teamChat==null)
            {
                return null;

            }

            return teamChat;

        }


        public async Task<ICollection<TeamChat>> GetTeamChatsOfUser(string userId)
        {
            var teamChats = await teamChatRepository.GetTeamChatByCreator(userId);

            if (teamChats.Count().Equals(0))
            {
                return null;

            }

            return teamChats;

        }


    }
}
