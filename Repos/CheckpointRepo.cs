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
        private readonly ApplicationContext _context;

        public CheckpointRepo(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<CheckPoint> DeleteCheckpointById(int checkpointId)
        {
            
            CheckPoint checkPoint = await _context.CheckPoints.FindAsync(checkpointId);
            if(checkPoint!=null)
            { 
               _context.CheckPoints.Remove(checkPoint);
                
            }
            return checkPoint;

        }

        public async Task<IEnumerable<CheckPoint>> GetAllCheckpointsByParentTask(int parentTaskId)
        {

            return (await _context.CheckPoints.Where(C => C.ParentRTaskId == parentTaskId).ToListAsync());
            
        }

        public async Task<CheckPoint> GetCheckPointById(int checkpointId)
        {
            return (await _context.CheckPoints.FirstOrDefaultAsync(C => C.Id == checkpointId));
        }




        public async Task SaveCheckPoint(CheckPoint checkpoint)
        {
            await _context.CheckPoints.AddAsync(checkpoint);
          

        }

        public async Task<CheckPoint> UpdateCheckpointById(int checkpointId,CheckPoint checkPoint)
        {
           var NewCheckpoint = await _context.CheckPoints.FindAsync(checkpointId);
            if(NewCheckpoint!=null)
            {
                NewCheckpoint.Text = checkPoint.Text;
                NewCheckpoint.IsChecked = checkPoint.IsChecked;
                NewCheckpoint.ParentRTaskId = checkPoint.ParentRTaskId;
                return NewCheckpoint;

            }
            return null;

        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

       
    }
}
