using System.Collections.Generic;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public interface IRTaskRepository
    {
         Task<IEnumerable<RTask>> GetAllTasksByTeam(int teamId);
         Task<IEnumerable<RTask>> GetAllTasksByCreator(int userId);
         Task<IEnumerable<RTask>> GetAllTasksByProject(int projectId);
         Task<IEnumerable<RTask>> GetAllSubTasksByParentCheckPointId(int checkpointId);
         Task<IEnumerable<RTask>> GetAllTasksByDependantTask(int dependantTaskId);
         Task<IEnumerable<RTask>> GetAllTasksByDependOnTask(int dependOnTaskId);
         Task<RTask> GetTaskById(int taskId);
         Task SaveTask(RTask task);
         Task UpdateTaskById(int taskId, RTask task);
         Task DeleteTaskById(int taskId);
     }
}