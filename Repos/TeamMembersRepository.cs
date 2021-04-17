using System.Collections.Generic;
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

        public async Task<ICollection<TeamsMembers>> DeleteTeamsMembersByTeam(int teamId)
        {
            var teamsMember = await context.TeamsMembers.Where(Tm => Tm.TeamId == teamId).ToListAsync();
            if (teamsMember.Count()!=0)
            {
                foreach (TeamsMembers teamsMembers in teamsMember)
                {
                    context.TeamsMembers.Remove(teamsMembers);
                }
            }
            return teamsMember;
        }

        public async Task<TeamsMembers> GetTeamMembersByUserIdAndTeamId(string userId, int teamId)
        {
            return await  context.TeamsMembers
                            .Where(tm => tm.UserId == userId && tm.TeamId == teamId)
                            .FirstOrDefaultAsync();
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}