using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Net;
using System.Threading.Tasks;
using work_platform_backend.Dtos;
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
        private readonly IHubContext<NotificationHub> notificationHub;
        private readonly IHubContext<ChatHub> chatHub;
        private readonly TeamChatService teamChatService;
        private readonly IMapper mapper;

        public TeamController(TeamService teamService, UserService userService, TaskService taskService, NotificationService notificationService, TeamChatService teamChatService, IHubContext<ChatHub> chatHub, IHubContext<NotificationHub> notificationHub, IMapper mapper)

        {
            this.teamService = teamService;
            this.userService = userService;
            this.taskService = taskService;

            this.notificationService = notificationService;
            this.teamChatService = teamChatService;
            this.chatHub = chatHub;
            this.notificationHub = notificationHub;
            this.mapper = mapper;
        }










        [HttpGet]
        [Route("{teamId}")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
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
        [Authorize(AuthenticationSchemes = "Bearer")]
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
        [Authorize(AuthenticationSchemes = "Bearer")]
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
        [Authorize(AuthenticationSchemes = "Bearer")]
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
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetTeamTasks(int teamId)
        {
             try
            {
                var tasksOfTeam = await teamService.GetTasksOfTeam(teamId);
                return Ok(tasksOfTeam);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

          

        }


        //THIS WILL BE ADDED WHEN WE USE NOTIFICATION SERVICE


        // [Authorize(AuthenticationSchemes = "Bearer")]
        // [HttpGet("request/create/team/{parentTeamId}")]
        // public async Task<IActionResult> RequestAddTeam(int parentTeamId)
        // {
        //     try
        //     {

        //         var team = await teamService.GetTeamOnlyById(parentTeamId);
        //         var parentTeamLeaderId = team.LeaderId;
        //         var userId = userService.GetUserId();

        //         if(userId == parentTeamLeaderId)
        //         {
        //             throw new Exception("you are the leader of the team");
        //         }

        //         var user = await userService.getUserById(userId);
            
        //         var notification = new Notification
        //         {
        //             Content = $"the user {user.Name} want to create a subteam from your team",
        //             Url = $"{this.Request.Host}/api/v1/teams/{team.RoomId}/{team.Id}/creator/{userId}",
        //             UserId = parentTeamLeaderId
        //         };

        //         notification = await notificationService.CreateNewNotificaition(notification);
        //         await notificationHub.Clients.User(parentTeamLeaderId).SendAsync("recievenotification",notification);

        //         return Ok();
        //     }   
        //     catch (Exception e)
        //     {
        //         return NotFound(e.Message);
        //     }

        // }

        ///<summary>
        /// add new team in room 
        ///</summary>
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        // [HttpPost("{roomId}/{parentTeamId}/creator/{userId}")]
        // [HttpPost("/rooms/{roomId}/{teamCode}/{parentTeamId}/connection/{connectionId}")]
        // public async Task<IActionResult> AddTeam(Team team, int roomId, int parentTeamId, string userId)
        // public async Task<IActionResult> AddTeam(Team team, int roomId, int parentTeamId,connectionId)
        [HttpPost("rooms/{roomId}/parentteam/{parentTeamId}")]
        // public async Task<IActionResult> AddTeam(Team team, int roomId, int parentTeamId)
        public async Task<IActionResult> AddTeam(int roomId, int parentTeamId, TeamDto teamDto)
        {
            try
            {
                var userId = "12131415Ma";
                // var newTeam = await teamService.AddTeam(team, roomId, userId,parentTeamId);
                var user = await userService.getUserById(userId);

                var team = mapper.Map<Team>(teamDto);
                var newTeam = await teamService.AddTeam(team,roomId, userId ,parentTeamId);
                
                var JoinChatOfTeam = await teamChatService.GetTeamChatOfTeam(newTeam.Id);

                //   await chatHub.Clients.Group(JoinChatOfTeam.ChatName).SendAsync("ReceiveMessageOnJoin", $"User: {user.UserName} Join Group of {JoinChatOfTeam} "); //Not Show to New User That Join  *Must Saved  inHistory
                //   await chatHub.Groups.AddToGroupAsync(chatHub.Clients.User(userId), JoinChatOfTeam.ChatName);  //add to Group to tell Clients on Group new User Come       
                var parentTeam = await teamService.GetTeamOnlyById(parentTeamId);
                // var notification = notificationService.CreateNewNotificaition(new Notification
                // {
                //     Content = $"the leader of team {parentTeam.Name} accept your request to creatث a sub team from this team",
                //     UserId = userId
                // });

                // await notificationHub.Clients.User(userId).SendAsync("recievenotification",notification);
                return Ok();

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);

            }

        }

        [HttpPut("{teamId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateTeam(int teamId, Team team)
        {
            try
            {   Team UpdatedTeam = await teamService.UpdateTeam(teamId, team);
                return Ok();
            }
            catch(Exception e)
            {
               return NotFound(e.Message);
            }
        }
        
        
        [HttpPut("{teamId}/deactivate")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeactivateTeam(int teamId)
        {
            try
            {   
                await teamService.DeactivateTeam(teamId);
                return Ok();
            }
            catch(Exception e)
            {
               return BadRequest(e.Message);
            }
        }


        [HttpDelete]
        [Route("{teamId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteTeam(int teamId)
        {
            try
            {
               await teamService.DeleteTeam(teamId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }


        [HttpPost("{teamId}/tasks")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        
        public async Task<IActionResult> AddTaskInTeam(int teamId, RTask task)
        {
            try
            {
                return Ok(await taskService.AddTaskInTeam(userService.GetUserId(), teamId, task));
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










