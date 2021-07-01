using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using work_platform_backend.Dtos;
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class SessionService
    {
        private readonly ISessionRepository sessionRepository;
        private readonly IMapper mapper;
        private readonly IRTaskRepository taskRepository;

        public SessionService(ISessionRepository sessionRepository, IMapper mapper, IRTaskRepository taskRepository)
        {
            this.sessionRepository = sessionRepository;
            this.mapper = mapper;
            this.taskRepository = taskRepository;
        }

        public async Task<List<SessionDto>> GetSessionsByTaskAndUser(string userId, int taskId)
        {

            var sessions =  await sessionRepository.GetSessionsByTaskAndUser(userId,taskId);

            return sessions.Select(s => mapper.Map<SessionDto>(s)).ToList();
        }



        public async Task<bool> DeleteSessionByTask(int taskId)
        {
            var sessions = await sessionRepository.DeleteSessionsByTask(taskId);


            if (sessions.Count().Equals(0))
            {
                return false;
            }

            return await sessionRepository.SaveChanges();
        }

        public async Task<int> OpenSession(int taskId, string userId)
        {
            Session lastSession = await sessionRepository.GetLastUserSession(userId);

            if(lastSession != null)
            {
                if(!LastSessionIsFinished(lastSession))
                {
                    throw new Exception("there is a Current open Session"); 
                }
            }

            Session session = await CreateNewSession(taskId, userId);
            await sessionRepository.SaveChanges();

            return session.Id;
        
        }

        private async Task<Session> CreateNewSession(int taskId, string userId)
        {
            Session session = new Session();

            session.TaskId = taskId;
            session.UserId = userId;
            session.StartDate = DateTime.Now;

            await sessionRepository.SaveSession(session);
            
            await AddActualStartToTask(taskId,session.StartDate);

            return session;
        }

        private async Task AddActualStartToTask(int taskId, DateTime startDate)
        {
            var task = await taskRepository.getTaskOnlyById(taskId);

            if(task == null)
            {
                throw new Exception("task not exist");
            }

            if(task.ActualStartDate == null) 
            {
                task.ActualStartDate = startDate;
                await taskRepository.SaveChanges();
            }

         
            
        }

        private bool LastSessionIsFinished(Session lastSession)
        {
            return lastSession.EndDate == null ? false : true;
        }



        public async Task<string> CloseSession(int sessionId, int extraMinuties)
        {
           var session =  await taskRepository.CloseSession(sessionId);
        
            if(session == null)
            {
                throw new Exception("session not exist");
            }

            session.EndDate = DateTime.Now;
            session.ExtraTime = extraMinuties;
            
            await taskRepository.SaveChanges();

            return "session closed successfully";
        }

        public async Task<Session> GetCurrentSession(string userId)
        {
            Session session = await taskRepository.GetCurrentSession(userId);

            if(session == null)
            {
                throw new Exception("there is no sessions availabe for this user");
            }

            //the session still opened
            if(session.EndDate == null)
            {
                return session;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Session>> GetSessionsBetweenTwoDates(string userId, string startDateTime, string endDateTime)
        {
            return await taskRepository.GetSessionBetweenStartDateAndEndDate(userId, startDateTime, endDateTime);
        }
    }
}