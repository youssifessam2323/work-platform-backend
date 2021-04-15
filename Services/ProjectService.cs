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
        private readonly IMapper mapper;
        private readonly TaskService taskService;
        private readonly ITeamRepository teamRepository;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper,TaskService taskService , ITeamRepository teamRepository)
        {
            this.projectRepository = projectRepository;
            this.mapper = mapper;
            this.taskService = taskService;
            this.teamRepository = teamRepository;
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
            Project newProject = await projectRepository.UpdateProjectById(id, project);

            if (newProject != null)
            {
                await projectRepository.SaveChanges();
                return newProject;
            }


            throw new Exception("project not exist");

        }

        public async Task AddTeamToProject(int projectId, int teamId)
        {
            await projectRepository.AddTeamToProject(projectId, teamId);
            await projectRepository.SaveChanges();
        }

        public async Task DeleteProject(int projectId)
        {
            if (!await projectRepository.IsProjectExist(projectId))
            {
                throw new Exception("project not exist");
            }
            var project = await projectRepository.DeleteProjectById(projectId);

            await taskService.DeleteTaskByProject(projectId);   //cause errors

            await projectRepository.RemoveTeamProjectbyProject(projectId);  //not cause exception


            await projectRepository.SaveChanges();

        }

        public async Task<bool> DeleteProjectByRoom(int roomId)
        {
            var projects = await projectRepository.DeleteProjectByRoom(roomId);
            if (projects.Count().Equals(0))
            {
                return false;
            }


            foreach (Project project in projects)
            {
                await DeleteProject(project.Id);
            }

            return true;
           

        }



        public async Task RemoveTeamToProject(int projectId, int teamId)
        {
            await projectRepository.RemoveTeamFromProject(projectId, teamId);
            await projectRepository.SaveChanges();
        }

        public async Task<IEnumerable<Project>> GetProjectsByRoom(int roomId)
        {
            var projects = await projectRepository.GetAllProjectsByRoom(roomId);

            if (projects.Count().Equals(0))
            {
                return null;

            }

            return projects;

        }

   

        public async Task<ProjectDetailsDto> GetProject(int projectId)
        {
            if (!await projectRepository.IsProjectExist(projectId))
            {
                throw new Exception("project not exist");
            }

            var project = await projectRepository.GetProjectById(projectId);



            return mapper.Map<ProjectDetailsDto>(project);

        }

        public async Task<Project> AddNewProjectToRoom(string userId, int roomId, Project project)
        {
            project.CreatorId = userId;
            project.RoomId = roomId;
            await projectRepository.SaveProject(project);
            await projectRepository.SaveChanges();
            return project;
        }

        public async Task<List<TeamDto>> GetAssignedTeams(int projectId)
        {
            if (!await projectRepository.IsProjectExist(projectId))
            {
                throw new Exception("project not exist");
            }

            var teams = await projectRepository.GetProjectAssignedTeams(projectId);

            return teams.Select(t => mapper.Map<TeamDto>(t)).ToList();
        }
    }
}
