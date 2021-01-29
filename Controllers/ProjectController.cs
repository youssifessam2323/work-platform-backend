using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Dtos;
using work_platform_backend.Models;
using work_platform_backend.Services;

namespace work_platform_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;
        private readonly TaskService _taskService;
       


        public ProjectController(ProjectService projectService, TaskService taskService)
        {
            _projectService = projectService;
            _taskService = taskService;
            

        }
        [HttpGet]
        [Route("GetProject/{projectId}")]
        public async Task<IActionResult> GetSingleProject(int projectId)
        {

            var project = await _projectService.GetProject(projectId);
            if (project == null)
            {
                return NotFound();

            }
            return Ok(project);

        }

        [HttpGet]
        [Route("GetProjectsInRoom/{RoomId}")]
        public async Task<IActionResult> GetProjectsInRoom(int RoomId)
        {

            var Projects = await _projectService.GetProjectsByRoom(RoomId);
            if (Projects != null)
            { 
            //     List<List<ResponseProjectTasksDto>> ListOfTasksinProject = new List<List<ResponseProjectTasksDto>>();
            //    foreach (var P in Projects)
            //    {

            //        ListOfTasksinProject.Add(await _taskService.GetTasksByProject(P.Id));

            //    }

                return Ok(Projects );

            }
            return Ok( new List<Project>());

        }


        [HttpPost("AddProject")]
        public async Task<IActionResult> AddProject(Project project)
        {
            var NewProject = await _projectService.AddProject(project);
            if (NewProject != null)
            {
                return Ok(NewProject);
            }
            return BadRequest();
        }

        [HttpPut("{ProjectId}")]
        public async Task<IActionResult> UpdateProject(int ProjectId, Project project)
        {
            Project UpdatedProject = await _projectService.UpdateProject(ProjectId, project);
            if (UpdatedProject == null)
            {
                return NotFound();
            }
            return Ok(UpdatedProject);

        }


        [HttpDelete]
        [Route("delete/{projectId}")]
        public async Task<IActionResult> DeletProject(int projectId)
        {
            try
            {
                await _projectService.DeleteProject(projectId);


            }
            catch (Exception Ex)
            {

                return NotFound(Ex.Message);
            }

            return Ok($"Object with id = {projectId} was  Deleted");
        }
    }
}
