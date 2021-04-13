﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using work_platform_backend.Dtos;
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class CheckPointService
    {
       
        private readonly ICheckpointRepository checkpointRepository;
        private readonly IRTaskRepository taskRepository;
        private readonly IMapper mapper;


        public CheckPointService(ICheckpointRepository checkpointRepository, IMapper mapper, IRTaskRepository taskRepository)
        {
            this.checkpointRepository = checkpointRepository;
            this.mapper = mapper;
            this.taskRepository = taskRepository;
        }



        public async Task<CheckPoint> UpdateCheckPoint(int id, CheckPoint checkPoint)
        {
            CheckPoint NewCheckpoint = await checkpointRepository.UpdateCheckpointById(id,checkPoint);

            if (NewCheckpoint != null)
            {
              await checkpointRepository.SaveChanges();

                return NewCheckpoint;
            }


            return null;

        }

        public async Task DeleteCheckPoint(int checkPointId)
        {
            var CheckPoint = await checkpointRepository.DeleteCheckpointById(checkPointId);
            if (CheckPoint == null)
            {

                throw new NullReferenceException();

            }

            await checkpointRepository.SaveChanges();


          var rTask =   await taskRepository.DeleteTaskBy_ParentCheckPoint(checkPointId);

            if(rTask!=null)
            {
                await taskRepository.SaveChanges();
            }

        }


        public async Task<IEnumerable<CheckPointDto>> GetCheckPointsofParentTask(int parentTaskId)
        {
            if(! await taskRepository.isTaskExist(parentTaskId))
            {
                throw new Exception("task not exist");
            }
            var Checkpoints = await checkpointRepository.GetAllCheckpointsByParentTaskId(parentTaskId);
            return Checkpoints.Select(c => mapper.Map<CheckPointDto>(c)).ToList();

          

        }


        public async Task<CheckPointDetailsDto> GetCheckPoint(int checkPointId)
        {
            if(! await checkpointRepository.IsCheckpointExist(checkPointId))
            {
                throw new Exception("checkpoint not exist");
            }
            var checkPoint = await checkpointRepository.GetCheckPointById(checkPointId);            

            return mapper.Map<CheckPointDetailsDto>(checkPoint);
        }

        public async Task<List<CheckPointDto>> GetCheckpointsByTask(int taskId)
        {
            if(!await taskRepository.isTaskExist(taskId))
            {
                throw new Exception("task not exist");
            }
           var checkpoints =  (List<CheckPoint>)await checkpointRepository.GetAllCheckpointsByParentTaskId(taskId);

           return checkpoints.Select(c => mapper.Map<CheckPointDto>(c)).ToList(); 

        }

        public async Task<CheckPoint> SaveNewCheckpointInTask(int taskId,CheckPointDto checkpointDto)
        {
             if(!await taskRepository.isTaskExist(taskId))
            {
                throw new Exception("task not exist");
            }
            var checkpoint = mapper.Map<CheckPoint>(checkpointDto);
            await checkpointRepository.SaveCheckPoint(taskId,checkpoint);
            await checkpointRepository.SaveChanges();
            return checkpoint;
        }
    }
}

