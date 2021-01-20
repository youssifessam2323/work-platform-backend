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
         Task UpdateProjectById(int projectId,Project project);
         Task DeleteProjectById(int projectId);
         
    }
}