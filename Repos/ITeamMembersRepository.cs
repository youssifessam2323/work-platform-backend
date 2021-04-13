using System.Collections.Generic;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public interface ITeamMembersRepository
    {
        Task<TeamsMembers> GetTeamMembersByUserIdAndTeamId(string userId,int teamId);
        Task<ICollection<TeamsMembers>> DeleteTeamsMembersByTeam(int teamId);
        Task SaveChanges();
    }
}