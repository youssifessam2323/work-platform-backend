using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using work_platform_backend.Dtos;
using work_platform_backend.Exceptions;
using work_platform_backend.Exceptions.Team;
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class TaskService
    {
        private readonly IRTaskRepository taskRepository;
        private readonly IMapper mapper;
        private ITeamRepository teamRepository;
        private IProjectRepository projectRepository;
        private ICheckpointRepository checkpointRepository;
        private readonly IUserRepository userRepository;
        private readonly SessionService sessionService;
        private readonly AttachmentService attachmentService;
        private readonly CommentService commentService;
        private readonly CheckPointService checkPointService;


        public TaskService(IRTaskRepository taskRepository, IMapper mapper, ITeamRepository teamRepository, IProjectRepository projectRepository, SessionService sessionService, AttachmentService attachmentService, CommentService commentService, CheckPointService checkPointService, ICheckpointRepository checkpointRepository, IUserRepository userRepository)
        {
            this.taskRepository = taskRepository;
            this.mapper = mapper;
            this.teamRepository = teamRepository;
            this.projectRepository = projectRepository;
            this.checkpointRepository = checkpointRepository;
            this.sessionService = sessionService;
            this.attachmentService = attachmentService;
            this.checkPointService = checkPointService;
            this.commentService = commentService;
            this.userRepository = userRepository;
        }


        public async Task<int> AddTaskInTeam(string userId,int teamId,RTask newTask)
        {
            if(! await teamRepository.IsTeamExist(teamId))
            {
                throw new Exception("team not exist");
            }
            if (newTask != null)
            {
                if(DateTime.Compare(newTask.PlannedStartDate,newTask.PlannedEndDate) >= 0){
                    throw new DateTimeException(newTask.PlannedStartDate.Date.ToString(),newTask.PlannedEndDate.Date.ToString());
                }
                newTask.CreatorId = userId;
                newTask.TeamId = teamId;
                await taskRepository.SaveTask(newTask);
                await taskRepository.SaveChanges();
            }

            return newTask.Id;
           
        }

        public async Task UpdateTask(int id, RTask task)
        {
            RTask UpdatedTask = await taskRepository.UpdateTaskById(id, task);

            if (UpdatedTask != null)
            {
                await taskRepository.SaveChanges();
            }
        }

        public async Task<bool> IsTaskExist(int taskId)
        {
            return await taskRepository.isTaskExist(taskId);

        }

        // public async Task<bool> DeleteTask(int taskId) 
        // {
            
           
        //     if (await IsTaskExist(taskId))
        //     {
        //         await checkPointService.DeleteCheckPointByParentTask(taskId);
        //         var task = await taskRepository.DeleteTaskById(taskId);
        //         await sessionService.DeleteSessionByTask(taskId);
        //         await attachmentService.DeleteAttachmentByTask(taskId);
        //         await commentService.DeleteCommentByTask(taskId);
        //         await taskRepository.RemoveUserTaksbyTask(taskId);
        //         return await taskRepository.SaveChanges();
        //     }



        //     return false;
      
           
        // }


        // public async Task<bool> DeleteTaskByTeam(int teamId)
        // {
        //   var Rtasks = await taskRepository.GetTasksByTeam(teamId);  

        //     if(Rtasks.Count().Equals(0))
        //     {
        //         return false;
        //     }

        //     foreach(RTask rTask in Rtasks)
        //     {
        //         await DeleteTask(rTask.Id);
        //     }

        //     return true;
        // }

        public async Task<int> AddNewSubTask(string userId ,int parentCheckpointId, RTask task)
        {
            if(!await checkpointRepository.IsCheckpointExist(parentCheckpointId))
            {
                throw new Exception("checkpoint not exist");
            }

            task.ParentCheckPointId = parentCheckpointId;
            task.CreatorId= userId;
            await taskRepository.SaveTask(task);
            await taskRepository.SaveChanges();

            return task.Id;
        }

        // public async Task<bool> DeleteTaskByProject(int projectId)
        // {
        //     var Rtasks = await taskRepository.GetAllTasksByProject(projectId);

        //     if (Rtasks.Count().Equals(0))
        //     {
        //         return false;
        //     }

        //     foreach (RTask rTask in Rtasks)
        //     {
        //         await DeleteTask(rTask.Id);
        //     }

        //     return true;
        // }

    


        public async Task<TaskDetailsDto> GetTask(int TaskId)
        {
            var task = await taskRepository.GetTaskById(TaskId);

            if (task == null)
            {
                throw new Exception("task not exist");
            }
            var taskDto = mapper.Map<TaskDetailsDto>(task);

            return taskDto;
        }

        public async Task<IEnumerable<TaskDto>> GetTaskByCreator(string CreatorId)
        {
            var tasks = await taskRepository.GetAllTasksByCreator(CreatorId);

            return tasks.Select(t => mapper.Map<TaskDto>(t)).ToList(); 
        }

        public async Task<IEnumerable<RTask>> GetTasksByTeam(int TeamId)
        {
            var Tasks = await taskRepository.GetTasksByTeam(TeamId);

            if (Tasks.Count().Equals(0))
            {
                return null;

            }

            return Tasks;

        }


        public async Task<IEnumerable<TaskDto>> GetTasksByProject(int projectId)
        {
            if(!await projectRepository.IsProjectExist(projectId))
            {
                throw new Exception("project not exist");
            }
            var tasks = await taskRepository.GetAllTasksByProject(projectId);
            return tasks.Select(t => mapper.Map<TaskDto>(t));

        }

        public async Task<List<TaskDto>> GetTaskDependantTasks(int taskId)
        {
            if(!await taskRepository.isTaskExist(taskId))
            {
                throw new Exception("task not exist");
            }

            var dependantTasks = (List<RTask>)await taskRepository.GetAllTasksByDependantTask(taskId);
            

            return dependantTasks.Select(t => mapper.Map<TaskDto>(t)).ToList();
        }

        public async Task<List<Comment>> GetTaskComments(int taskId)
        {
            return await taskRepository.GetTaskComments(taskId);
        }

        public async Task<IEnumerable<TaskDto>> GetSubTasksByParentCheckPoint(int checkpointId)
        {
            if (!await checkPointService.IsCheckpointExist(checkpointId))
            {
                throw new Exception("checkpoint not exist");
            }
            var tasks = await taskRepository.GetAllSubTasksByParentCheckPointId(checkpointId);
            return tasks.Select(t => mapper.Map<TaskDto>(t)).ToList();

        }

        public async Task<List<TaskDto>> GetTasksAssignedToUserInTeam(string userId, int teamId)
        {
            var team = await teamRepository.GetTeamById(teamId);
            if(team == null)
            {
                throw new TeamNotFoundException("team not exist");
            }
            var tasks = await taskRepository.GetTasksByUserIdAndTeamId(userId,teamId);
            return tasks.Select(t => mapper.Map<TaskDto>(t)).ToList();
        }

      
        public async Task<RTask> AddTaskInProject(string userId, int projectId, RTask newTask)
        {
            if (newTask != null)
            {
                newTask.CreatorId = userId;
                newTask.ProjectId = projectId;
                await taskRepository.SaveTask(newTask);
                await taskRepository.SaveChanges();
                return newTask;
            }
            throw new NullReferenceException("task must be valid");
        }

        public async Task<List<UserDto>> GetUsersAssignedToTaskByTaskId(int taskId)
        {
            List<User> users =  await taskRepository.GetTaskAssignedUsers(taskId);
            
            return users.Select(u => mapper.Map<UserDto>(u)).ToList();
        }

        public async Task AssignedUsersToTaskByTaskId(int taskId, List<string> usersUsername)
        {

            Console.WriteLine("usernames from frontend is ==========> ");

            foreach ( var i in usersUsername)
            {
            Console.WriteLine("i ===> " + i );
            }
            var task = await taskRepository.getTaskOnlyById(taskId);
            List<User> users = new List<User>();
            

            foreach(string username in usersUsername)
            {
                var user = await userRepository.GetUserByUsername(username);
                    Console.WriteLine($"user is  {user} ");
                if(user == null)
                {
                    Console.WriteLine($"user {username} is null ");
                    continue;
                }
                users.Add(user);        
            }

            // Console.WriteLine(" the data processed by the Server ==========> " + JsonSerializer.Serialize(users));

            foreach(var user in users )
            {
                Console.WriteLine(" user Id is   ==========> " + user.Id);
                var isUserAssignedInTask  = await  userRepository.IsUserIsAssignedInThisTask(user.Id,taskId);
                if(isUserAssignedInTask)
                {
                    continue;
                }
                else
                {
                    await taskRepository.AssignUsersToASpecificTask(taskId,user.Id);
                    await taskRepository.SaveChanges();
                }
            }


        }

        public async Task RemoveAssignedUsersToTaskByTaskId(int taskId, List<string> usersUsername)
        {
            Console.WriteLine("usernames from frontend is ==========> ");

                foreach ( var i in usersUsername)
                {
                Console.WriteLine("i ===> " + i );
                }
                var task = await taskRepository.getTaskOnlyById(taskId);
                List<User> users = new List<User>();
                

                foreach(string username in usersUsername)
                {
                    var user = await userRepository.GetUserByUsername(username);

                    if(user == null)
                    {
                        throw new Exception($"user {username} not exist");
                    }
                    users.Add(user);        
                }

                // Console.WriteLine(" the data processed by the Server ==========> " + JsonSerializer.Serialize(users));

                foreach(var user in users )
                {
                    Console.WriteLine(" user Id is   ==========> " + user.Id);
                    await taskRepository.RemoveAssignUsersToASpecificTask(taskId,user.Id);
                    await taskRepository.SaveChanges();
                }
        }
    }
}
