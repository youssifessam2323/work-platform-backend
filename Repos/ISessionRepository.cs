using System.Collections.Generic;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public interface ISessionRepository
    {
        Task<List<Session>> GetSessionsByTaskAndUser(string userId, int taskId);

        Task<List<Session>> DeleteSessionsByTask(int taskId);
        Task<bool> SaveChanges();


    }
}