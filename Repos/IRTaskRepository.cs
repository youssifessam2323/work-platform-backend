using System.Collections.Generic;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public interface IRTaskRepository
    {
         Task<IEnumerable<RTask>> GetAllTasksByCreator(string userId);
         Task<IEnumerable<RTask>> GetAllTasksByProject(int projectId);
         Task<IEnumerable<RTask>> GetAllSubTasksByParentCheckPointId(int checkpointId);
         Task<IEnumerable<RTask>> GetAllTasksByDependantTask(int dependantTaskId);
         Task<IEnumerable<RTask>> GetAllTasksByDependOnTask(int dependOnTaskId);
         Task<RTask> GetTaskById(int taskId);
         Task SaveTask(RTask task);
         Task<RTask> UpdateTaskById(int taskId, RTask task);
         Task<RTask> DeleteTaskById(int taskId);
        Task<bool> RemoveUserTaksbyTask(int taskId);
        Task<bool> SaveChanges();
        Task<List<RTask>> GetTasksByTeam(int teamId);
        Task<List<Comment>> GetTaskComments(int taskId);
        Task<List<RTask>> GetTasksByUserIdAndTeamId(string userId, int teamId);
        Task<List<User>> GetTaskAssignedUsers(int taskId);
        Task<bool> isTaskExist(int taskId);
        Task<RTask> getTaskOnlyById(int taskId);
        Task AssignUsersToASpecificTask(int taskId, string  userId);
        Task RemoveAssignUsersToASpecificTask(int taskId, string id);
        Task<Session> CloseSession( int sessionId);
        Task<Session> GetCurrentSession(string userId);
        Task<List<Session>> GetSessionBetweenStartDateAndEndDate(string userId, string startDateTime, string endDateTime);
        // Task CloseSession(int taskId);
    }
}