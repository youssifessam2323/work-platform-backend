using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public class TeamMembersRepository : ITeamMembersRepository
    {
        private ApplicationContext context;

        public TeamMembersRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<TeamsMembers> GetTeamMembersByUserIdAndTeamId(string userId, int teamId)
        {
            return await  context.TeamsMembers
                            .Where(tm => tm.UserId == userId && tm.TeamId == teamId)
                            .FirstOrDefaultAsync();
        }
    }
}