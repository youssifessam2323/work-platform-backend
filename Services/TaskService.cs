using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Dtos;
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class TaskService
    {
        private readonly IRTaskRepository _RTaskRepository;
        private readonly IMapper _mapper;

        public TaskService(IRTaskRepository RTaskRepository,IMapper mapper)
        {
            _RTaskRepository = RTaskRepository;
            _mapper = mapper;
        }


        public async Task<RTask> AddTask(RTask newTask)
        {
            if (newTask != null)
            {
                await _RTaskRepository.SaveTask(newTask);
                await _RTaskRepository.SaveChanges();
                return newTask;
            }
            return null;

        }

        public async Task<RTask> UpdateTask(int id, RTask task)
        {
            RTask UpdatedTask = await _RTaskRepository.UpdateTaskById(id, task);

            if (UpdatedTask != null)
            {
                await _RTaskRepository.SaveChanges();
                return UpdatedTask;
            }


            return null;

        }


        public async Task DeleteTask(int taskId)
        {
            var Task = await _RTaskRepository.DeleteTaskById(taskId);
            if (Task == null)
            {

                throw new NullReferenceException();

            }

            await _RTaskRepository.SaveChanges();


        }

        public async Task<RTask> GetTask(int TaskId)
        {
            var Task = await _RTaskRepository.GetTaskById(TaskId);

            if (Task!=null)
            {
                return Task;

            }

            return null;

        }

        public async Task<IEnumerable<RTask>> GetTaskByCreator(string CreatorId)
        {
            var Tasks = await _RTaskRepository.GetAllTasksByCreator(CreatorId);

            if (Tasks.Count().Equals(0))
            {
                return null;

            }

            return Tasks;

        }

        public async Task<IEnumerable<RTask>> GetTasksByTeam(int TeamId)
        {
            var Tasks = await _RTaskRepository.GetAllTasksByTeam(TeamId);

            if (Tasks.Count().Equals(0))
            {
                return null;

            }

            return Tasks;

        }


        public async Task<IEnumerable<ResponseProjectTasksDto>> GetTasksByProject(int ProjectId)
        {
            var Tasks = await _RTaskRepository.GetAllTasksByProject(ProjectId);

            if (Tasks.Count().Equals(0))
            {
                return null;

            }
            var TaskInProjectsResponse = _mapper.Map< IEnumerable<ResponseProjectTasksDto>>(Tasks);

            return TaskInProjectsResponse;

        }

        public async Task<IEnumerable<RTask>> GetSubTasksByParentCheckPoint(int checkpointId)
        {
            var Tasks = await _RTaskRepository.GetAllSubTasksByParentCheckPointId(checkpointId);

            if (Tasks.Count().Equals(0))
            {
                return null;

            }

            return Tasks;

        }


    }
}
