﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public class ProjectRepo : IProjectRepository
    {
        private readonly ApplicationContext context;

        public ProjectRepo(ApplicationContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Project>> GetAllProjectsByRoom(int roomId)
        {
            return (await context.Projects.Include(P=>P.Tasks).ThenInclude(t => t.Team).Include(p => p.Creator)
                .Where(P => P.Room.Id == roomId).ToListAsync());
        }

        public async Task<Project> GetProjectById(int projectId)
        {
          return( await context.Projects.FirstOrDefaultAsync(P => P.Id == projectId));
        }

        

        public async Task SaveProject(Project project)
        {
           await context.Projects.AddAsync(project);
        }

        public async Task<Project> UpdateProjectById(int projectId, Project project)
        {
            var NewProject = await context.Projects.FindAsync(projectId);
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
            Project project = await context.Projects.FindAsync(projectId);
            if (project != null)
            {
                context.Projects.Remove(project);
            }
            return project;
        }

        public async Task<bool> SaveChanges()
        {
            return (await context.SaveChangesAsync() >= 0);
        }

        public async Task<List<Project>> GetProjectByTeam(int teamId)
        {
            List<TeamProject> teamProjects = await context.TeamProjects.Include(tp => tp.Project).Where(tp => tp.TeamId == teamId ).ToListAsync();
            List<Project> projects = new List<Project>();

            teamProjects.ForEach(tp => projects.Add(tp.Project));
            return projects;
        }

        public async Task AddTeamToProject(int projectId, int teamId)
        {
            TeamProject teamProject = new TeamProject();
            teamProject.TeamId = teamId;
            teamProject.ProjectId = projectId;
            await context.TeamProjects.AddAsync(teamProject);
            
        }
    }
}
