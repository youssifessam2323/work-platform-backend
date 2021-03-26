using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Dtos;
using work_platform_backend.Exceptions;
using work_platform_backend.Models;
using work_platform_backend.Services;

namespace work_platform_backend.Controllers
{
    [Route("api/v1/projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService projectService;
        private readonly TaskService taskService;
        private readonly UserService userService;

        public ProjectController(ProjectService projectService, TaskService taskService, UserService userService)
        {
            this.projectService = projectService;
            this.taskService = taskService;
            this.userService = userService;
        }


        [HttpPost]
        [Route("{projectId}/teams/{teamId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddTeamToProject(int projectId,int teamId)
        {   
            try
            {
            await projectService.AddTeamToProject(projectId,teamId);
            }
            catch(DbUpdateException e)
            {
                return BadRequest("this team is already in this project");
            }
            return Ok();
        }


       

        [HttpGet]
        [Route("GetProject/{projectId}")]
        public async Task<IActionResult> GetSingleProject(int projectId)
        {

            var project = await projectService.GetProject(projectId);
            if (project == null)
            {
                return NotFound();

            }
            return Ok(project);

        }


       

        [HttpPut("{ProjectId}")]
        public async Task<IActionResult> UpdateProject(int ProjectId, Project project)
        {
            Project UpdatedProject = await projectService.UpdateProject(ProjectId, project);
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
                await projectService.DeleteProject(projectId);


            }
            catch (Exception Ex)
            {

                return NotFound(Ex.Message);
            }

            return Ok($"Object with id = {projectId} was  Deleted");
        }


        [HttpPost("{projectId}/tasks")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddTaskInProject(int projectId, RTask task)
        {
            try
            {
                var newTask = await taskService.AddTaskInProject(userService.GetUserId(), projectId, task);

                if (newTask != null)
                {
                    return Ok(newTask);
                }
            }
            catch (DateTimeException e)
            {
                return BadRequest(e.Message);
            }
            return BadRequest();
        }
    }
}
