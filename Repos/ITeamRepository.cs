using System.Collections.Generic;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetAllTeamsByRoom(int roomId);    
        Task<IEnumerable<Team>> GetAllTeamsByMember(string userId);
        Task<IEnumerable<Team>> GetAllTeamsByCreator(string userId);
        Task<Team> GetTeamById(int teamId);       
        Task SaveTeam(Team team);
        Task <Team>UpdateTeamById(int teamId,Team team);
        Task<Team> DeleteTeamById(int teamId);
        Task<bool> SaveChanges();
        Task<Team> GetTeamByTeamCode(string teamCode);
        Task<List<Team>> GetTeamSubTeamsById(int teamId);
        Task<List<User>> GetMembersOfTeam(int teamId);

        Task<bool> isUserinThisTeamExist(int teamId, string userId);
        Task<Team> GetTeamOnlyById(int teamId);

        Task<bool> IsTeamExist(int teamId);

    }
}