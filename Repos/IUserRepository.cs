using System.Collections.Generic;
using System.Threading.Tasks;
using work_platform_backend.Dtos.Response;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public interface IUserRepository
    {
        Task<User> GetUserById(string id);
        Task SaveNewTeamMember(User user, Team team);
        Task SaveChanges();
        Task<List<Team>> getUserTeams(string userId);
    }
}