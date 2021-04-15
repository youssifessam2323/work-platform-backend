using System.Collections.Generic;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public interface IProjectRepository
    {
         Task<IEnumerable<Project>> GetAllProjectsByRoom(int roomId);
         Task<Project> GetProjectById(int projectId);
         Task SaveProject(Project project);
         Task<Project> UpdateProjectById(int projectId,Project project);
         Task <Project>DeleteProjectById(int projectId);
        Task<List<Project>> DeleteProjectByRoom(int roomId);
        Task<bool> SaveChanges();
        Task<List<Project>> GetProjectByTeam(int teamId);
        Task AddTeamToProject(int projectId, int teamId);
        Task<List<Team>> GetProjectAssignedTeams(int projectId);
        Task RemoveTeamFromProject(int projectId, int teamId);
        public Task<bool> RemoveTeamProjectbyProject(int projectId);
        Task<bool> IsProjectExist(int projectId);
    }
}