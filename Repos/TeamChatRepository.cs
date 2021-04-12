using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public class TeamChatRepository : ITeamChatRepository
    {
        private readonly ApplicationContext context;
        private readonly ChatMessageRepository chatMessageRepository;

        public TeamChatRepository(ApplicationContext context,ChatMessageRepository chatMessageRepository)
        {
            this.context = context;
            this.chatMessageRepository = chatMessageRepository;
        }

        public async Task SaveTeamChat(TeamChat teamChat)
        {
            await context.TeamChats.AddAsync(teamChat);
        }
        public async Task<TeamChat> DeleteTeamChatById(int TeamChatId)
        {
            TeamChat teamChat = await context.TeamChats.FindAsync(TeamChatId);
            if (teamChat != null)
            {
                context.TeamChats.Remove(teamChat);
                await chatMessageRepository.DeleteAllMessageByTeamCHat(TeamChatId);
              
            }
            return teamChat;
        }

        public async Task<TeamChat> DeleteTeamChatByTeamId(int teamId)
        {
            TeamChat teamChat =  await context.TeamChats.Where(Tc=>Tc.TeamId ==teamId).FirstOrDefaultAsync();
            if (teamChat != null)
            {
                context.TeamChats.Remove(teamChat);
            }
            return teamChat;
        }

        public async Task<ICollection<TeamChat>> GetTeamChatByCreator(string CreatorId)
        {
           return(await context.TeamChats.Where(Tc => Tc.CreatorId == CreatorId).ToListAsync());
          
        }

        public async Task<TeamChat> GetTeamChatById(int TeamchatId)
        {
            return (await context.TeamChats.FirstOrDefaultAsync(Tc => Tc.Id == TeamchatId));
        }

        public async Task<TeamChat> GetTeamChatByTeam(int teamId)
        {

            return (await context.TeamChats.Where(Tc => Tc.TeamId == teamId).FirstAsync());
        }

        public async Task<bool> SaveChanges()
        {
            return (await context.SaveChangesAsync() >= 0);
        }

      
    }
}
