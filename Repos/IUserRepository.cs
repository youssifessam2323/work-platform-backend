using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public interface IUserRepository
    {
        Task<User> GetUserById(string id);
        Task SaveNewTeamMember(string userId, int teamId);
        Task DeleteTeamMember(string user, int teamId);
        
        Task SaveChanges();
        Task<List<Team>> getUserTeams(string userId);
        Task<User> GetUserByUsername(string username);
        Task<bool> IsUserExistByUsername(string username);
        Task<bool> IsUserExistById(string userId);
        Task<User> UpdateUser(string userId, User newUser);
    }
}