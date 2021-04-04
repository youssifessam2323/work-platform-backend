using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public interface IUserRepository
    {
        Task<User> GetUserById(string id);
        Task SaveNewTeamMember(User user, Team team);
        Task SaveChanges();
        Task<List<Team>> getUserTeams(string userId);
        Task<User> GetUserByUsername(string username);
        Task<User> UpdateUser(string userId, User newUser);
    }
}