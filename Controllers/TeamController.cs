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
     

        public TeamController(TeamService teamService, UserService userService, TaskService taskService )
        {
            this.teamService = teamService;
            this.userService = userService;
            this.taskService = taskService;
           
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        [Route("GetTeamsCreator")]
        public async Task<IActionResult> GetTeamsCreator()
        {
            string teamCreatorId = userService.GetUserId();

            var GetTeamsByCreator = await teamService.GetTeamsByCreator(teamCreatorId);
            if (GetTeamsByCreator == null)
            {
                return Ok(new List<Team>());

            }
            return Ok(GetTeamsByCreator);

        }


        [HttpGet]
        [Route("{teamId}")]
        public async Task<IActionResult> GetTeamById(int teamId)
        {

            var Team = await teamService.GetTeam(teamId);
            if (Team == null)
            {
                return NotFound(string.Format("No Team Found with this Id = {0} ",teamId));

            }
            return Ok(Team);

        }


        [HttpGet]
        [Route("{teamId}/subteams")]
        public async Task<IActionResult> GetTeamSubTeams(int teamId)
        {

            var Team = await teamService.GetTeamSubTeams(teamId);
            if (Team.Count == 0)
            {
                return Ok(new List<Team>());
            }
            return Ok(Team);

        }

        [HttpGet]
        [Route("{teamId}/members")]
        public async Task<IActionResult> GetTeamMembers(int teamId)
        {

            var members = await teamService.GetMembersOfTeam(teamId);
            return Ok(members);

        }

        [HttpGet]
        [Route("{teamId}/projects")]
        public async Task<IActionResult> GetTeamProjects(int teamId)
        {

            var projects = await teamService.GetProjectsOfTeam(teamId);
            return Ok(projects);

        }

        [HttpGet]
        [Route("{teamId}/tasks")]
        public async Task<IActionResult> GetTeamTasks(int teamId)
        {

            var projects = await teamService.GetTasksOfTeam(teamId);
            return Ok(projects);

        }



        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("{roomId}")]
        public async Task<IActionResult> AddTeam(Team team, int roomId)
        {
            try
            {
                var newTeam = await teamService.AddTeam(team, roomId, userService.GetUserId());
                if (newTeam != null)
                {
                    return Ok(newTeam);
                }
                return BadRequest();
            }
            catch (DbUpdateException e)
            {
                return BadRequest("Room Does not exist");
            }

        }

        [HttpPut("{TeamId}")]
        public async Task<IActionResult> UpdateTeam(int TeamId, Team team)
        {
            Team UpdatedTeam = await teamService.UpdateTeam(TeamId, team);
            if (UpdatedTeam == null)
            {
                return NotFound();
            }
            return Ok(UpdatedTeam);

        }


        [HttpDelete]
        [Route("{teamId}")]
        public async Task<IActionResult> DeletTeam(int teamId)
        {
            try
            {
                await teamService.DeleteTeam(teamId);
            }
            catch (DbUpdateException ex)
            {
                
                return NotFound("You cannot delete this team because it has subteams, go and delete subteams before deleting it.");
            }

            return Ok($"Object with id = {teamId} was  Deleted");
        }


        [HttpPost("{teamId}/tasks")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddTaskInTeam(int teamId, RTask task)
        {
            try
            {
                var newTask = await taskService.AddTaskInTeam(userService.GetUserId(), teamId, task);

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


        [HttpGet]
        [Route("{teamId}/tasks")]
        public async Task<IActionResult> GetTasksOfTeam(int teamId)
        {

            var tasksByTeam = await taskService.GetTasksByTeam(teamId);
            if (tasksByTeam == null)
            {
                return NotFound();

            }
            return Ok(tasksByTeam);

        }

    }

 




}

