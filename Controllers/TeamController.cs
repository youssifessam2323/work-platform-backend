using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using work_platform_backend.Exceptions;
using work_platform_backend.Models;
using work_platform_backend.Services;

namespace work_platform_backend.Controllers
{
    [Route("api/v1/teams")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly TeamService teamService;
        private readonly UserService userService;
        private readonly TaskService taskService;


        public TeamController(TeamService teamService, UserService userService, TaskService taskService)
        {
            this.teamService = teamService;
            this.userService = userService;
            this.taskService = taskService;

        }




        [HttpGet]
        [Route("{teamId}")]
        public async Task<IActionResult> GetTeamById(int teamId)
        {
            try
            {
                var team = await teamService.GetTeam(teamId);
                return Ok(team);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);

            }


        }



        [HttpGet]
        [Route("{teamId}/subteams")]
        public async Task<IActionResult> GetTeamSubTeams(int teamId)
        {
            try
            {
                var Team = await teamService.GetTeamSubTeams(teamId);
                return Ok(Team);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        [Route("{teamId}/members")]
        public async Task<IActionResult> GetTeamMembers(int teamId)
        {
            try
            {
                var members = await teamService.GetMembersOfTeam(teamId);
                return Ok(members);

            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }

        [HttpGet]
        [Route("{teamId}/projects")]
        public async Task<IActionResult> GetTeamProjects(int teamId)
        {
            try
            {
                var projects = await teamService.GetProjectsOfTeam(teamId);
                return Ok(projects);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }


        }

        [HttpGet]
        [Route("{teamId}/tasks")]
        public async Task<IActionResult> GetTeamTasks(int teamId)
        {
             try
            {
                var projects = await teamService.GetTasksOfTeam(teamId);
                return Ok(projects);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

          

        }


        // this will alter later(to add permission feature)
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("{roomId}")]
        public async Task<IActionResult> AddTeam(Team team, int roomId)
        {
            try
            {
                var newTeam = await teamService.AddTeam(team, roomId, userService.GetUserId());
                    return Ok();
            }
            catch(Exception e)
            {
               return NotFound(e.Message);
            }

        }

        [HttpPut("{TeamId}")]
        public async Task<IActionResult> UpdateTeam(int TeamId, Team team)
        {
            try
            {   Team UpdatedTeam = await teamService.UpdateTeam(TeamId, team);
                return Ok();
            }
            catch(Exception e)
            {
               return NotFound(e.Message);
            }
        }


        [HttpDelete]
        [Route("{teamId}")]
        public async Task<IActionResult> DeletTeam(int teamId)
        {
            try
            {
                await teamService.DeleteTeam(teamId);
                return Ok();
            }
            catch (DbUpdateException ex)
            {

                return Unauthorized("You cannot delete this team because it has subteams, go and delete subteams before deleting it.");
            }

        }


        [HttpPost("{teamId}/tasks")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddTaskInTeam(int teamId, RTask task)
        {
            try
            {
                await taskService.AddTaskInTeam(userService.GetUserId(), teamId, task);
                    return Ok();
            }
            catch(NullReferenceException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }



    }






}

