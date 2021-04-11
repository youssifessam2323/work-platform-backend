using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public interface IUserRepository
    {
        Task<User> GetUserById(string id);
<<<<<<< HEAD
        Task SaveNewTeamMember(User user, Team team);
        void DeleteTeamMember(User user, Team team);
        Task SaveChanges();
        Task<List<Team>> getUserTeams(string userId);
        Task<User> GetUserByUsername(string username);
=======
        Task SaveNewTeamMember(string userId, int teamId);
        Task SaveChanges();
        Task<List<Team>> getUserTeams(string userId);
        Task<User> GetUserByUsername(string username);
        Task<bool> IsUserExistByUsername(string username);
        Task<bool> IsUserExistById(string userId);
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
        Task<User> UpdateUser(string userId, User newUser);
    }
}