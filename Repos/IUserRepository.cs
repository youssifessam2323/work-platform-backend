using System.Collections.Generic;
using System.Threading.Tasks;
using work_platform_backend.Dtos;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public interface IUserRepository
    {
        public Task<User> AddNewUser(User user);
        public IEnumerable<User> GetAllUsers();
        public bool SaveChanges();
        Task<User> GetUserByEmail(string Email);
        ICollection<Role> getUserRoleByUsername(string username);
        Task<User> getUserByToken(string token);
    }
}