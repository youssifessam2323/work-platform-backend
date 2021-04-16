using System.Collections.Generic;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public interface ICheckpointRepository
    {
         Task<IEnumerable<CheckPoint>> GetAllCheckpointsByParentTaskId(int parentTaskId);
        Task<CheckPoint> GetCheckPointById(int checkpointId);
         Task SaveCheckPoint(int taskId, CheckPoint checkpoint);
         Task <CheckPoint>UpdateCheckpointById(int checkpointId,CheckPoint checkPoint);
         Task <CheckPoint> DeleteCheckpointById(int checkpointId);
         Task <bool>SaveChanges();
        Task<bool> IsCheckpointExist(int checkPointId);
    }
}