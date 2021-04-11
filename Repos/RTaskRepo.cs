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
        private readonly ApplicationContext context;

        public RTaskRepo(ApplicationContext context)
        {
            this.context = context;
        }
      
        public async Task<IEnumerable<RTask>> GetAllSubTasksByParentCheckPointId(int checkpointId)
        {
           return( await context.Tasks.Where(T => T.ParentCheckPointId == checkpointId).ToListAsync());
        }

        public async Task<IEnumerable<RTask>> GetAllTasksByCreator(string userId)
        {
            return (await context.Tasks.Where(T => T.Creator.Id == userId).ToListAsync());
        }

        public async Task<IEnumerable<RTask>> GetAllTasksByDependantTask(int taskId)
        {
            var task = await context.Tasks.Include(t => t.DependantTasks).Where(t => t.Id == taskId).SingleAsync();
            return task.DependantTasks;
        }

        public Task<IEnumerable<RTask>> GetAllTasksByDependOnTask(int dependOnTaskId)
        {
          throw new NotImplementedException();
        }

        public async Task<IEnumerable<RTask>> GetAllTasksByProject(int projectId)
        {
            return (await context.Tasks
                            .Include(t => t.ChildCheckPoints)
                            .ThenInclude(cs => cs.SubTasks)
                            .Where(T => T.Project.Id == projectId)
                            .ToListAsync());
        }

        public async Task<IEnumerable<RTask>> GetAllTasksByTeam(int teamId)
        {
            return (await context.Tasks.Where(T => T.Team.Id == teamId).ToListAsync());
        }

        public async Task<RTask> GetTaskById(int taskId)
        {
            return (await context.Tasks.FirstOrDefaultAsync(T => T.Id == taskId));
        }

      
        public async Task SaveTask(RTask task)
        {
           await context.Tasks.AddAsync(task);
        }

        public async Task<RTask> UpdateTaskById(int taskId, RTask task)
        {
            var newTask = await context.Tasks.FindAsync(taskId);
            if (newTask != null)
            {
                newTask.Name = task.Name;
                newTask.Description = task.Description;
                newTask.PlannedStartDate = task.PlannedStartDate;
                newTask.PlannedEndDate = task.PlannedEndDate;
                newTask.ActualStartDate = task.ActualStartDate;
                newTask.ActualEndDate = task.ActualEndDate;
                newTask.IsFinished = task.IsFinished;
                newTask.ParentCheckPointId = task.ParentCheckPointId;
                
               context.Tasks.Update(newTask);
               return newTask;
            }
            return null;
        }


        //not completed
        public async Task DeleteTaskById(int taskId)
        {

            RTask task = await context.Tasks.Include(t => t.DependantTasks)
                                    .Include(t => t.Comments)
                                    .ThenInclude(c => c.Replies)
                                    .Where(t => t.Id == taskId)
                                    .FirstAsync();
            
            Console.WriteLine(task);
            task.Comments.ForEach(c => Console.WriteLine(c));
            task.Comments.ForEach( async c => {
                c.Replies.ForEach(r =>  context.Comments.Remove(r));
                await context.SaveChangesAsync();
                context.Comments.Remove(c);
            });

            foreach(var dependantTask in task.DependantTasks)
            {
                await DeleteTaskById(dependantTask.Id);
            }
            if (task != null)
            {
                context.Tasks.Remove(task);

            }
        }


        public async Task<bool> SaveChanges()
        {
            return (await context.SaveChangesAsync() >= 0);
        }

        public async Task<List<RTask>> GetTasksByTeam(int teamId)
        {
            return await context.Tasks.Where(t => t.TeamId == teamId).ToListAsync();
        }

        public Task<List<Comment>> GetTaskComments(int taskId)
        {
            return context.Comments.Where(c => c.TaskId == taskId).ToListAsync();
        }

        public async Task<List<RTask>> GetTasksByUserIdAndTeamId(string userId, int teamId)
        {
            List<UserTask> userTasks = await context.UserTasks.Include(ut => ut.Task).Where(ut => ut.UserId == userId).ToListAsync(); 
            List<RTask> tasks = new List<RTask>();
            userTasks.ForEach(ut => {
                tasks.Add(ut.Task);
            });
            return tasks.Where(t => t.TeamId == teamId).ToList();
        }
<<<<<<< HEAD
=======

        public async Task<List<User>> GetTaskAssignedUsers(int taskId)
        {
            return await context.UserTasks.Where(ut => ut.TaskId ==  taskId).Select(ut => ut.User).ToListAsync();
        }
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
    }
}
