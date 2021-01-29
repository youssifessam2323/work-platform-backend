using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public class RTaskRepo : IRTaskRepository
    {
        private readonly ApplicationContext _context;

        public RTaskRepo(ApplicationContext context)
        {
            _context = context;
        }
      
        public async Task<IEnumerable<RTask>> GetAllSubTasksByParentCheckPointId(int checkpointId)
        {
           return( await _context.Tasks.Where(T => T.ParentCheckPointId == checkpointId).ToListAsync());
        }

        public async Task<IEnumerable<RTask>> GetAllTasksByCreator(string userId)
        {
            return (await _context.Tasks.Where(T => T.Creator.Id == userId).ToListAsync());
        }

        public Task<IEnumerable<RTask>> GetAllTasksByDependantTask(int dependantTaskId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RTask>> GetAllTasksByDependOnTask(int dependOnTaskId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RTask>> GetAllTasksByProject(int projectId)
        {
            return (await _context.Tasks.Include(T=>T.Team).Where(T => T.Project.Id == projectId).ToListAsync());
        }

        public async Task<IEnumerable<RTask>> GetAllTasksByTeam(int teamId)
        {
            return (await _context.Tasks.Where(T => T.Team.Id == teamId).ToListAsync());
        }

        public async Task<RTask> GetTaskById(int taskId)
        {
            return (await _context.Tasks.FirstOrDefaultAsync(T => T.Id == taskId));
        }

      
        public async Task SaveTask(RTask task)
        {
           await _context.Tasks.AddAsync(task);
        }

        public async Task<RTask> UpdateTaskById(int taskId, RTask task)
        {
            var NewTask = await _context.Tasks.FindAsync(taskId);
            if (NewTask != null)
            {
                NewTask.Name = task.Name;
                NewTask.Description = task.Description;
                NewTask.PlannedStartDate = task.PlannedStartDate;
                NewTask.PlannedEndDate = task.PlannedEndDate;
                NewTask.ActualStartDate = task.ActualStartDate;
                NewTask.ActualEndDate = task.ActualEndDate;
                NewTask.IsFinished = task.IsFinished;
                NewTask.ParentCheckPointId = task.ParentCheckPointId;

            }
            return null;
        }

        public async Task<RTask> DeleteTaskById(int taskId)
        {

            RTask task = await _context.Tasks.FindAsync(taskId);
            if (task != null)
            {
                _context.Tasks.Remove(task);

            }
            return task;
        }


        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

    }
}
