using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public class ProjectRepo : IProjectRepository
    {
        private readonly ApplicationContext _context;

        public ProjectRepo(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Project>> GetAllProjectsByRoom(int roomId)
        {
            return (await _context.Projects.Include(P=>P.Tasks).ThenInclude(t => t.Team)
                .Where(P => P.Room.Id == roomId).ToListAsync());
        }

        public async Task<Project> GetProjectById(int projectId)
        {
          return( await _context.Projects.FirstOrDefaultAsync(P => P.Id == projectId));
        }

        

        public async Task SaveProject(Project project)
        {
           await _context.Projects.AddAsync(project);
        }

        public async Task<Project> UpdateProjectById(int projectId, Project project)
        {
            var NewProject = await _context.Projects.FindAsync(projectId);
            if (NewProject != null)
            {
                NewProject.Name = project.Name;
                NewProject.Description = project.Description;
                NewProject.PlannedStartDate = project.PlannedStartDate;
                NewProject.PlannedEndDate = project.PlannedEndDate;
                NewProject.ActualStartDate = project.ActualStartDate;
                NewProject.ActualEndDate = project.ActualEndDate;
                NewProject.IsFinished = project.IsFinished;

                return NewProject;
            }
            return null;
        }

        public async Task<Project> DeleteProjectById(int projectId)
        {
            Project project = await _context.Projects.FindAsync(projectId);
            if (project != null)
            {
                _context.Projects.Remove(project);
            }
            return project;
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

    }
}
