using AutoMapper;
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
        private readonly IProjectRepository _ProjectRepo;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository,  IMapper mapper)
        {
            _ProjectRepo = projectRepository;
            _mapper = mapper;

        }


        public async Task<Project> AddProject(Project newProject)
        {
            if (newProject != null)
            {
                await _ProjectRepo.SaveProject(newProject);
                await _ProjectRepo.SaveChanges();
                return newProject;
            }
            return null;

        }

        public async Task<Project> UpdateProject(int id, Project project)
        {
            Project NewProject = await _ProjectRepo.UpdateProjectById(id, project);

            if (NewProject != null)
            {
                await _ProjectRepo.SaveChanges();
                return NewProject;
            }


            return null;

        }


        public async Task DeleteProject(int projectId)
        {
            var Team = await _ProjectRepo.DeleteProjectById(projectId);
            if (Team == null)
            {

                throw new NullReferenceException();

            }

            await _ProjectRepo.SaveChanges();


        }


        public async Task<IEnumerable<ResponseProjectDto>> GetProjectsByRoom(int roomId)
        {
            var Projects = await _ProjectRepo.GetAllProjectsByRoom(roomId);

            if (Projects.Count().Equals(0))
            {
                return null;

            }
            var ProjectsResponse = _mapper.Map<IEnumerable<ResponseProjectDto>>(Projects);

            return ProjectsResponse;

        }

        public async Task<Project> GetProject(int projectId)
        {
            var Project = await _ProjectRepo.GetProjectById(projectId);

            if (Project==null)
            {
                return null;

            }

            return Project;

        }

    }
}
