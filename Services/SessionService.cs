using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using work_platform_backend.Dtos;
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class SessionService
    {
        private readonly ISessionRepository sessionRepository;
        private readonly IMapper mapper;

        public SessionService(ISessionRepository sessionRepository, IMapper mapper)
        {
            this.sessionRepository = sessionRepository;
            this.mapper = mapper;
        }

        public async Task<List<SessionDto>> GetSessionsByTaskAndUser(string userId, int taskId)
        {

            var sessions =  await sessionRepository.GetSessionsByTaskAndUser(userId,taskId);

            return sessions.Select(s => mapper.Map<SessionDto>(s)).ToList();
        }
    }
}