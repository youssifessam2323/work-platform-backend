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
           return( await context.Tasks
                                    .Include(t => t.ChildCheckPoints)
                                    .Where(T => T.ParentCheckPointId == checkpointId).ToListAsync());
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

        public async Task<List<RTask>> GetTasksByTeam(int teamId)
        {
            return (await context.Tasks
                                    .Include(t => t.ChildCheckPoints)
                                    .Include(t => t.ParentCheckPoint)
                                    .Include( t => t.UserTasks).ThenInclude(ut => ut.User)
                                    .Include(t => t.Creator)
                                    .Where(T => T.Team.Id == teamId)
                                    .ToListAsync());
        }

        



        public async Task<RTask> GetTaskById(int taskId)
        {
            return (await context.Tasks
                                    .Include(t => t.Attachments)
                                    .Include(t => t.Creator)
                                    .Include(t => t.Project)
                                    .Include(t =>  t.DependantTasks)
                                    .Include(t => t.ParentCheckPoint)
                                    .Include(t => t.UserTasks).ThenInclude(ut => ut.User)
                                    .Include(t => t.Comments)
                                    .Include(t => t.Sessions)
                                    .Include(t => t.ChildCheckPoints)
                                    .Include(t => t.Team)
                                    .FirstOrDefaultAsync(t => t.Id == taskId));
        }

      
        public async Task SaveTask(RTask task)
        {
           await context.Tasks.AddAsync(task);
        }


        // not working
        public async Task<RTask> UpdateTaskById(int taskId, RTask task)
        {
            var newTask = await context.Tasks
                                    .Include(t => t.ChildCheckPoints)
                                    .Where( t => t.Id == taskId).SingleOrDefaultAsync();
            if (newTask != null)
            {

                if(task.Name != null)
                    newTask.Name = task.Name;
                
                if(task.Description != null)
                    newTask.Description = task.Description;
                
                if(!(DateTime.Compare(task.PlannedStartDate,new DateTime(1,1,1)) == 0))
                {
                    Console.WriteLine("planned start date ==============>" + task.PlannedStartDate);
                    newTask.PlannedStartDate = task.PlannedStartDate;
                }
                
                if(!(DateTime.Compare(task.PlannedEndDate,new DateTime(1,1,1)) == 0))
                {
                    Console.WriteLine("planned end date ==============>" + task.PlannedEndDate);
                    newTask.PlannedEndDate = task.PlannedEndDate;
                }
              
                
                if(task.ChildCheckPoints != null)
                {
                    foreach(var c in task.ChildCheckPoints)
                    {
                        
                        if(c.Id == 0)
                        {
                            c.ParentRTaskId = taskId;
                            await context.CheckPoints.AddAsync(c);
                        }
                        else
                        {
                             var  checkpointNeededToUpdate =await context
                                    .CheckPoints
                                    .Where( checkpoint => checkpoint.Id ==  c.Id )
                                    .SingleOrDefaultAsync();


                            if(checkpointNeededToUpdate == null)
                            {
                                throw new Exception(" checkpoints not exist");
                            }

                            if(c.CheckpointText != null)
                            {
                                checkpointNeededToUpdate.CheckpointText  = c.CheckpointText;
                            } 

                            if(c.Description != null)
                            {
                                checkpointNeededToUpdate.Description  = c.Description;
                            }

                            if(c.Percentage != 0)
                            {
                                checkpointNeededToUpdate.Percentage  = c.Percentage;
                            } 

                        }

  
                    

                    }

                }
               return newTask;
            }
            return null;
        }


        //not completed
        public async Task<RTask> DeleteTaskById(int taskId)
        {
            

            RTask task = await context.Tasks.Include(t => t.DependantTasks)                              
                                    .Where(t => t.Id == taskId)
                                    .FirstOrDefaultAsync();

           
            

            foreach(var dependantTask in task.DependantTasks)
            {
                await DeleteTaskById(dependantTask.Id);
            }
            if (task != null)
            {
                context.Tasks.Remove(task);

            }

            return task;

          


            

        }

      

    


        public async Task<bool> RemoveUserTaksbyTask(int taskId)
        {
            var userTasks = await context.UserTasks.Where(t => t.TaskId == taskId).ToListAsync();

            if (userTasks.Count().Equals(0))
            {
                return false;
            }
            foreach (UserTask t in userTasks)
            {
                context.UserTasks.Remove(t);
            }

            return true;
        }


        public async Task<bool> SaveChanges()
        {
            return (await context.SaveChangesAsync() >= 0);
        }


        public Task<List<Comment>> GetTaskComments(int taskId)
        {
            return context.Comments.Where(c => c.TaskId == taskId).ToListAsync();
        }

        public async Task<List<RTask>> GetTasksByUserIdAndTeamId(string userId, int teamId)
        {
            List<UserTask> userTasks = await context.UserTasks
                                                        .Include(ut => ut.Task).ThenInclude(t => t.ChildCheckPoints)
                                                        .Include(ut => ut.Task).ThenInclude(t => t.ParentCheckPoint)
                                                        .Include(ut => ut.Task).ThenInclude(t => t.Creator)
                                                        .Where(ut => ut.UserId == userId).ToListAsync(); 
            List<RTask> tasks = new List<RTask>();
            userTasks.ForEach(ut => {
                tasks.Add(ut.Task);
            });
            return tasks.Where(t => t.TeamId == teamId).ToList();
        }

        public async Task<List<User>> GetTaskAssignedUsers(int taskId)
        {
            return await context.UserTasks.Where(ut => ut.TaskId ==  taskId).Select(ut => ut.User).ToListAsync();
        }

        public async Task<bool> isTaskExist(int taskId)
        {
            var task = await context.Tasks.FindAsync(taskId);

            return task != null ? true : false ; 
        }

        public async Task<RTask> getTaskOnlyById(int taskId)
        {
            return await context.Tasks.Where(t => t.Id == taskId).SingleOrDefaultAsync();
        }

        public async Task AssignUsersToASpecificTask(int taskId, string userId)
        {
            var userTask = new UserTask();
            userTask.UserId = userId;
            userTask.TaskId = taskId;

            await context.UserTasks.AddAsync(userTask);

        }

        public async Task RemoveAssignUsersToASpecificTask(int taskId, string userId)
        {
          var userTask = await context.UserTasks.Where(ut => ut.UserId == userId && ut.TaskId == taskId).SingleOrDefaultAsync();
           
           if(userTask == null)
           {
               throw new Exception("this user in not assigned in this task");
           }

           context.UserTasks.Remove(userTask);

        }

        public async Task<Session> CloseSession(int sessionId)
        {
           var session = await  context.Sessions
                                        .FindAsync(sessionId);


            return session;
        }

        public async Task<Session> GetCurrentSession(string userId)
        {
            var session = await context.Sessions
                                    .Include(s => s.Task).ThenInclude( t => t.Team)
                                    .Include(s => s.Task).ThenInclude( t => t.ChildCheckPoints)
                                    .Where(s => s.UserId == userId)
                                    .OrderByDescending(s => s.StartDate)
                                    .FirstOrDefaultAsync();


            if(session.EndDate == DateTime.MaxValue)
            {
                session.EndDate = null;
            }                        

            return session;
        }

        public async Task<List<Session>> GetSessionBetweenStartDateAndEndDate(string userId, string startDateTime, string endDateTime)
        {
            Console.WriteLine("start date from dateTime instance " + DateTime.Parse(startDateTime));


            var sessions =  await context.Sessions 
                                    .Include(s => s.Task).ThenInclude(t => t.ChildCheckPoints)
                                    .Include(s => s.Task).ThenInclude(t => t.Team)
                                    .Where(s => s.UserId == userId
                                                        && s.StartDate >= DateTime.Parse(startDateTime)
                                                        && s.StartDate <= DateTime.Parse(endDateTime))
                                                        .ToListAsync();

            foreach(var s in sessions)
            {
                if(s.EndDate == DateTime.MinValue)
                {
                    s.EndDate = null;
                }
            }                                                        
                                    
            return sessions;                        
        }
    }
}
