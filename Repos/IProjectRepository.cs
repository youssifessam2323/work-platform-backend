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
        Task<bool> SaveChanges();
        Task<List<Project>> GetProjectByTeam(int teamId);
        Task AddTeamToProject(int projectId, int teamId);
    }
}