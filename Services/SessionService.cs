using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class SessionService
    {
        private readonly ISessionRepository sessionRepository;

        public SessionService(ISessionRepository sessionRepository)
        {
            this.sessionRepository = sessionRepository;
        }

        public async Task<List<Session>> GetSessionsByTaskAndUser(string userId, int taskId)
        {
            return await sessionRepository.GetSessionsByTaskAndUser(userId,taskId);
        }
    }
}