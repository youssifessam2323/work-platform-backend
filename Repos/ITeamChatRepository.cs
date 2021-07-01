using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public interface ITeamChatRepository
    {
        Task SaveTeamChat(TeamChat teamChat);
        Task<TeamChat> DeleteTeamChatById(int TeamChatId);
        Task<TeamChat> DeleteTeamChatByTeamId(int TeamId);

        Task<TeamChat> GetTeamChatById(int TeamchatId);
        Task<TeamChat> GetTeamChatByTeam(int teamId);
        Task<ICollection<TeamChat>> GetTeamChatByCreator(string CreatorId);

        Task<bool> SaveChanges();



    }
}
