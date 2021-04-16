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


        [HttpGet]
        [Route("{projectId}/add/teams/{teamId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddTeamToProject(int projectId,int teamId)
        {   
            try
            {
            await projectService.AddTeamToProject(projectId,teamId);
            return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        
        [HttpGet]
        [Route("{projectId}/remove/teams/{teamId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> RemoveTeamFromProject(int projectId,int teamId)
        {   
            try
            {
            await projectService.RemoveTeamToProject(projectId,teamId);
            return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


       

        [HttpGet]
        [Route("{projectId}")]
        public async Task<IActionResult> GetSingleProject(int projectId)
        {
            try
            {
                var project = await projectService.GetProject(projectId);
                return Ok(project);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
            

        }


       

        [HttpPut("{projectId}")]
        public async Task<IActionResult> UpdateProject(int projectId, Project project)
        {
            try
            {
                Project UpdatedProject = await projectService.UpdateProject(projectId, project);
               return Ok(UpdatedProject);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }

        }

        // not working
        [HttpDelete]
        [Route("{projectId}")]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            try
            {
                await projectService.DeleteProject(projectId);
                return Ok();
            }
            catch (Exception e)
            {

                return NotFound(e.Message);
            }

            
        }


        [HttpPost("{projectId}/tasks")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddTaskInProject(int projectId, RTask task)
        {
            try
            {
                var newTask = await taskService.AddTaskInProject(userService.GetUserId(), projectId, task);
                    return Ok(newTask);
            }
            catch (NullReferenceException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }


        [HttpGet("{projectId}/tasks")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetTasksInProject(int projectId)
        {
            try
            {
            return Ok(await taskService.GetTasksByProject(projectId));
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }


        [HttpGet("{projectId}/teams")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetTeamsAssignedInProject(int projectId)
        {
            return Ok(await projectService.GetAssignedTeams(projectId));
        }
    }
}
