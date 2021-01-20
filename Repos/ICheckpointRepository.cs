using System.Collections.Generic;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public interface ICheckpointRepository
    {
         Task<IEnumerable<CheckPoint>> GetAllCheckpointsByParentTask(int parentTaskId);
         Task SaveCheckPoint(CheckPoint checkpoint);
         Task UpdateCheckpointById(int checkpointId ,CheckPoint checkpoint);
         Task DeleteCheckpointById(int checkpointId);
    }
}