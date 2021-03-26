using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class CheckPointService
    {
       
        private readonly ICheckpointRepository checkpointRepository;
        

        public CheckPointService(ICheckpointRepository checkpointRepository)
        {
            this.checkpointRepository = checkpointRepository;
           
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


        }


        public async Task<IEnumerable<CheckPoint>> GetCheckPointsofParentTask(int ParentTaskId)
        {
            var Checkpoints = await checkpointRepository.GetAllCheckpointsByParentTaskId(ParentTaskId);

            if (Checkpoints.Count().Equals(0))
            {
                return null;

            }

            return Checkpoints;

          

        }


        public async Task<CheckPoint> GetCheckPoint(int checkPointId)
        {
            var Checkpoints = await checkpointRepository.DeleteCheckpointById(checkPointId);

            if (Checkpoints!=null)
            {
                return Checkpoints;

            }
            return null;
            

        }

        public async Task<List<CheckPoint>> GetCheckpointsByTask(int taskId)
        {
           return (List<CheckPoint>)await checkpointRepository.GetAllCheckpointsByParentTaskId(taskId);
        }

        public async Task<CheckPoint> SaveNewCheckpointInTask(int taskId,CheckPoint checkpoint)
        {
            await checkpointRepository.SaveCheckPoint(taskId,checkpoint);
            await checkpointRepository.SaveChanges();
            return checkpoint;
        }
    }
}

