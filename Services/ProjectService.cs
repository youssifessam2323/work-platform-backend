using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Dtos;
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class ProjectService
    {
        private readonly IProjectRepository projectRepository;
        private readonly IMapper _mapper;


        public ProjectService(IProjectRepository projectRepository,  IMapper mapper)
        {
            this.projectRepository = projectRepository;
            _mapper = mapper;

        }


        public async Task<Project> AddProject(Project newProject)
        {
            if (newProject != null)
            {
                await projectRepository.SaveProject(newProject);
                await projectRepository.SaveChanges();
                return newProject;
            }
            return null;

        }

        public async Task<Project> UpdateProject(int id, Project project)
        {
            Project NewProject = await projectRepository.UpdateProjectById(id, project);

            if (NewProject != null)
            {
                await projectRepository.SaveChanges();
                return NewProject;
            }


            return null;

        }

        public async Task AddTeamToProject(int projectId, int teamId)
        {
            await projectRepository.AddTeamToProject(projectId,teamId);
            await projectRepository.SaveChanges();
        }

        public async Task DeleteProject(int projectId)
        {
            var Team = await projectRepository.DeleteProjectById(projectId);
            if (Team == null)
            {

                throw new NullReferenceException();

            }

            await projectRepository.SaveChanges();


        }


        public async Task<IEnumerable<ResponseProjectDto>> GetProjectsByRoom(int roomId)
        {
            var Projects = await projectRepository.GetAllProjectsByRoom(roomId);

            if (Projects.Count().Equals(0))
            {
                return null;

            }
            var ProjectsResponse = _mapper.Map<IEnumerable<ResponseProjectDto>>(Projects);

            return ProjectsResponse;

        }

        public async Task<Project> GetProject(int projectId)
        {
            var Project = await projectRepository.GetProjectById(projectId);

            if (Project==null)
            {
                return null;

            }

            return Project;

        }

        public async Task<Project> AddNewProjectToRoom(string userId, int roomId, Project project)
        {
            project.CreatorId = userId;
            project.RoomId = roomId;
            await projectRepository.SaveProject(project);
            await projectRepository.SaveChanges();
            return project;
        }
    }
}
