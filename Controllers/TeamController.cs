using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using work_platform_backend.Exceptions;
using work_platform_backend.Hubs;
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
        private readonly NotificationService notificationService;
        private readonly IHubContext<NotificationHub> hub;


        public TeamController(TeamService teamService, UserService userService, TaskService taskService, NotificationService notificationService, IHubContext<NotificationHub> hub)
        {
            this.teamService = teamService;
            this.userService = userService;
            this.taskService = taskService;
            this.notificationService = notificationService;
            this.hub = hub;
        }

      


        [HttpGet]
        [Route("{teamId}")]
        public async Task<IActionResult> GetTeamById(int teamId)
        {
            try
            {
            var Team = await teamService.GetTeam(teamId);
            return Ok(Team);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
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
        [HttpGet("request/create/team/{parentTeamId}")]
        public async Task<IActionResult> RequestAddTeam(int parentTeamId)
        {
            try
            {
                var team = await teamService.GetTeamOnlyById(parentTeamId);
                var parentTeamLeaderId = team.LeaderId;
                var userId = userService.GetUserId();

                if(userId == parentTeamLeaderId)
                {
                    throw new Exception("you are the leader of the team");
                }

                var user = await userService.getUserById(userId);
            
                var notification = new Notification
                {
                    Content = $"the user {user.Name} want to create a subteam from your team",
                    Url = $"{this.Request.Host}/api/v1/teams/{team.RoomId}/{team.Id}/creator/{userId}",
                    UserId = parentTeamLeaderId
                };

                notification = await notificationService.CreateNewNotificaition(notification);
                await hub.Clients.User(parentTeamLeaderId).SendAsync("recievenotification",notification);

                return Ok();
            }   
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("{roomId}/{parentTeamId}/creator/{userId}")]
        public async Task<IActionResult> AddTeam(Team team, int roomId, int parentTeamId, string userId)
        {
            try
            {
                var newTeam = await teamService.AddTeam(team, roomId, userService.GetUserId(),parentTeamId);
                var parentTeam = await teamService.GetTeamOnlyById(parentTeamId);
                var notification = notificationService.CreateNewNotificaition(new Notification
                {
                    Content = $"the leader of team {parentTeam.Name} accept your request to creat a sub team from this team",
                    UserId = userId
                });

                await hub.Clients.User(userId).SendAsync("recievenotification",notification);
                return Ok();
            }
            catch (DbUpdateException e)
            {
                return BadRequest(e.Message);
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
        public async Task<IActionResult> DeleteTeam(int teamId)
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

    }

 




}

