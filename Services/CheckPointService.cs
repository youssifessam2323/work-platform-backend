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
       
        private readonly ICheckpointRepository _checkpointRepo;
        

        public CheckPointService(ICheckpointRepository checkpointRepository)
        {
            _checkpointRepo = checkpointRepository;
           
        }


        public async Task<CheckPoint> AddCheckpoint(CheckPoint newCheckpoint)
        {
            if (newCheckpoint != null)
            {
                await _checkpointRepo.SaveCheckPoint(newCheckpoint);
                await _checkpointRepo.SaveChanges();
                return newCheckpoint;
            }
            return null;

        }

        public async Task<CheckPoint> UpdateCheckPoint(int id, CheckPoint checkPoint)
        {
            CheckPoint NewCheckpoint = await _checkpointRepo.UpdateCheckpointById(id,checkPoint);

            if (NewCheckpoint != null)
            {
              await _checkpointRepo.SaveChanges();

                return NewCheckpoint;
            }


            return null;

        }

        public async Task DeleteCheckPoint(int checkPointId)
        {
            var CheckPoint = await _checkpointRepo.DeleteCheckpointById(checkPointId);
            if (CheckPoint == null)
            {

                throw new NullReferenceException();

            }

            await _checkpointRepo.SaveChanges();


        }


        public async Task<IEnumerable<CheckPoint>> GetCheckPointsofParentTask(int ParentTaskId)
        {
            var Checkpoints = await _checkpointRepo.GetAllCheckpointsByParentTask(ParentTaskId);

            if (Checkpoints.Count().Equals(0))
            {
                return null;

            }

            return Checkpoints;

          

        }


        public async Task<CheckPoint> GetCheckPoint(int checkPointId)
        {
            var Checkpoints = await _checkpointRepo.DeleteCheckpointById(checkPointId);

            if (Checkpoints!=null)
            {
                return Checkpoints;

            }
            return null;
            

        }





    }
}

