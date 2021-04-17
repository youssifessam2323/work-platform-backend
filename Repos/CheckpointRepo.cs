using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public class CheckpointRepo : ICheckpointRepository
    {
        private readonly ApplicationContext context;

        public CheckpointRepo(ApplicationContext context)
        {
            this.context = context;
            
        }

    

        
        public async Task<IEnumerable<CheckPoint>> GetAllCheckpointsByParentTaskId(int parentTaskId)
        {
            return await context.CheckPoints.Include(c => c.SubTasks).Where(c => c.ParentRTaskId == parentTaskId).ToListAsync();
        }

        public async Task<CheckPoint> GetCheckPointById(int checkpointId)
        {
            return (await context.CheckPoints
                                            .Include(c => c.ParentRTask)
                                            .Include(c => c.SubTasks)
                                            .FirstOrDefaultAsync(C => C.Id == checkpointId));
        }




        public async Task SaveCheckPoint(int taskId, CheckPoint checkpoint)
        {
            checkpoint.ParentRTaskId = taskId;
            await context.CheckPoints.AddAsync(checkpoint);
        }

        public async Task<CheckPoint> UpdateCheckpointById(int checkpointId,CheckPoint checkPoint)
        {
           var NewCheckpoint = await context.CheckPoints.FindAsync(checkpointId);
            if(NewCheckpoint!=null)
            {
                NewCheckpoint.CheckpointText = checkPoint.CheckpointText;
                NewCheckpoint.Percentage = checkPoint.Percentage;
                // NewCheckpoint.ParentRTaskId = checkPoint.ParentRTaskId;
                return NewCheckpoint;

            }
            return null;

        }

        public async Task<CheckPoint> DeleteCheckpointById(int checkpointId)
        {

            CheckPoint checkPoint = await context.CheckPoints.FindAsync(checkpointId);
            if (checkPoint != null)
            {
                context.CheckPoints.Remove(checkPoint);

              


            }
            return checkPoint;
        }



        public async Task<bool> SaveChanges()
        {
            return (await context.SaveChangesAsync() >= 0);
        }

        public async Task<bool> IsCheckpointExist(int checkPointId)
        {
            var c = await context.CheckPoints.FindAsync(checkPointId);

            return c != null ? true : false ;
        }

      
    }
}
