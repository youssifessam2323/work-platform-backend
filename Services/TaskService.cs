﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Dtos;
using work_platform_backend.Exceptions;
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class TaskService
    {
        private readonly IRTaskRepository taskRepository;
        private readonly IMapper mapper;

        public TaskService(IRTaskRepository taskRepository,IMapper mapper)
        {
            this.taskRepository = taskRepository;
            this.mapper = mapper;
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

        public async Task<RTask> UpdateTask(int id, RTask task)
        {
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

        public async Task<RTask> GetTask(int TaskId)
        {
            var Task = await taskRepository.GetTaskById(TaskId);

            if (Task!=null)
            {
                return Task;

            }

            return null;

        }

        public async Task<IEnumerable<RTask>> GetTaskByCreator(string CreatorId)
        {
            var Tasks = await taskRepository.GetAllTasksByCreator(CreatorId);

            if (Tasks.Count().Equals(0))
            {
                return null;

            }

            return Tasks;

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


        public async Task<IEnumerable<ResponseProjectTasksDto>> GetTasksByProject(int ProjectId)
        {
            var Tasks = await taskRepository.GetAllTasksByProject(ProjectId);

            if (Tasks.Count().Equals(0))
            {
                return null;

            }
            var TaskInProjectsResponse = mapper.Map< IEnumerable<ResponseProjectTasksDto>>(Tasks);

            return TaskInProjectsResponse;

        }

        public async Task<List<RTask>> GetTaskDependantTasks(int taskId)
        {
            var dependantTasks = (List<RTask>)await taskRepository.GetAllTasksByDependantTask(taskId);
            
            //retrieve recursivly all task dependant task 
            //but do not work
            foreach(var dependantTask in dependantTasks)
            {
                 dependantTask.DependantTasks = (List<RTask>)await taskRepository.GetAllTasksByDependantTask(dependantTask.Id);
            }
            return dependantTasks;
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

        public async Task<List<RTask>> GetTasksAssignedToUserInTeam(string userId, int teamId)
        {
            return await taskRepository.GetTasksByUserIdAndTeamId(userId,teamId);
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
    }
}
