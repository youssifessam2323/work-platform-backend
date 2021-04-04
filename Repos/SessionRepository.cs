using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public class SessionRepository : ISessionRepository
    {
        private readonly ApplicationContext context;

        public SessionRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<List<Session>> GetSessionsByTaskAndUser(string userId, int taskId)
        {
            return await context.Sessions.Where(s => s.UserId == userId && s.TaskId == taskId).ToListAsync();

        }
    }
}