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

        public TaskService(IRTaskRepository taskRepository, IMapper mapper, ITeamRepository teamRepository)
        {
            this.taskRepository = taskRepository;
            this.mapper = mapper;
            this.teamRepository = teamRepository;
        }


        public async Task<RTask> AddTaskInTeam(string userId,int teamId,RTask newTask)
        {
            if (newTask != null)
            {
                if(DateTime.Compare(newTask.PlannedStartDate,newTask.PlannedEndDate) >= 0){
                    throw new DateTimeException(newTask.PlannedStartDate.Date.ToString(),newTask.PlannedEndDate.Date.ToString());
                }
                newTask.CreatorId = userId;
                newTask.TeamId = teamId;
                await taskRepository.SaveTask(newTask);
                await taskRepository.SaveChanges();
                return newTask;
            }
            return null;

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


        public async Task DeleteTask(int taskId)
        {
            await taskRepository.DeleteTaskById(taskId);
            await taskRepository.SaveChanges();
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
            var Tasks = await taskRepository.GetAllTasksByTeam(TeamId);

            if (Tasks.Count().Equals(0))
            {
                return null;

            }

            return Tasks;

        }


        public async Task<IEnumerable<RTask>> GetTasksByProject(int ProjectId)
        {
            var tasks = await taskRepository.GetAllTasksByProject(ProjectId);
            return tasks;

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

        public async Task<IEnumerable<RTask>> GetSubTasksByParentCheckPoint(int checkpointId)
        {
            var Tasks = await taskRepository.GetAllSubTasksByParentCheckPointId(checkpointId);

            if (Tasks.Count().Equals(0))
            {
                return null;

            }

            return Tasks;

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
                if(DateTime.Compare(newTask.PlannedStartDate,newTask.PlannedEndDate) >= 0){
                    throw new DateTimeException(newTask.PlannedStartDate.Date.ToString(),newTask.PlannedEndDate.Date.ToString());
                }
                newTask.CreatorId = userId;
                newTask.ProjectId = projectId;
                await taskRepository.SaveTask(newTask);
                await taskRepository.SaveChanges();
                return newTask;
            }
            return null;
        }

        public async Task<List<UserDto>> GetUsersAssignedToTaskByTaskId(int taskId)
        {
            List<User> users =  await taskRepository.GetTaskAssignedUsers(taskId);
            
            return users.Select(u => mapper.Map<UserDto>(u)).ToList();
        }

    }
}
