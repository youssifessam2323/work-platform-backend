using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private readonly SessionService sessionService;
        private readonly AttachmentService attachmentService;
        private readonly CommentService commentService;
        private readonly CheckPointService checkPointService;


        public TaskService(IRTaskRepository taskRepository, IMapper mapper, ITeamRepository teamRepository, IProjectRepository projectRepository, SessionService sessionService, AttachmentService attachmentService, CommentService commentService, CheckPointService checkPointService, ICheckpointRepository checkpointRepository)
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
        }


        public async Task AddTaskInTeam(string userId,int teamId,RTask newTask)
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
            throw new NullReferenceException("insert valid task");
        }

        public async Task<RTask> UpdateTask(int id, TaskDetailsDto taskDetailsDto)
        {
            var task = mapper.Map<RTask>(taskDetailsDto);
            RTask UpdatedTask = await taskRepository.UpdateTaskById(id, task);
            if (UpdatedTask != null)
            {
                await taskRepository.SaveChanges();
                return UpdatedTask;
            }


            return null;

        }

        public async Task<bool> IsTaskExist(int taskId)
        {
            return await taskRepository.isTaskExist(taskId);

        }

        public async Task<bool> DeleteTask(int taskId) 
        {
            
           
            if (await IsTaskExist(taskId))
            {
                await checkPointService.DeleteCheckPointByParentTask(taskId);
                var task = await taskRepository.DeleteTaskById(taskId);
                await sessionService.DeleteSessionByTask(taskId);
                await attachmentService.DeleteAttachmentByTask(taskId);
                await commentService.DeleteCommentByTask(taskId);
                await taskRepository.RemoveUserTaksbyTask(taskId);
                return await taskRepository.SaveChanges();
            }



            return false;
      
           
        }


        public async Task<bool> DeleteTaskByTeam(int teamId)
        {
          var Rtasks = await taskRepository.GetTasksByTeam(teamId);  

            if(Rtasks.Count().Equals(0))
            {
                return false;
            }

            foreach(RTask rTask in Rtasks)
            {
                await DeleteTask(rTask.Id);
            }

            return true;
        }


        public async Task<bool> DeleteTaskByProject(int projectId)
        {
            var Rtasks = await taskRepository.GetAllTasksByProject(projectId);

            if (Rtasks.Count().Equals(0))
            {
                return false;
            }

            foreach (RTask rTask in Rtasks)
            {
                await DeleteTask(rTask.Id);
            }

            return true;
        }

    


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

    }
}
