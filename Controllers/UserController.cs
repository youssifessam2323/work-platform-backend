using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using work_platform_backend.Models;
using work_platform_backend.Services;

namespace work_platform_backend.Controllers
{

    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly UserService userService ;
        private readonly TaskService taskService;

        private TeamService teamService { get; set; }



        public UserController(UserService userService, TeamService teamService, TaskService taskService)
        {
            this.userService = userService;
            this.teamService = teamService;
            this.taskService = taskService;
        }


        [HttpGet]
        [Route("current")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetAuthenticatiedUser()
        {
            return Ok( await userService.getUserById(userService.GetUserId()));    
        }

        [HttpGet]
        [Route("rooms/{roomId}/teams")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> TeamsOfAuthUserInRoom(int roomId)
        {   
            return Ok(await userService.getTeamOfUserInRoom(roomId,userService.GetUserId()));
        }


        [HttpGet]
        [Route("join/teams/{teamCode}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> JoinTeam(string teamCode)
        {
            try{
            await userService.JoinTeam(teamCode,userService.GetUserId());
            
            }catch(Exception e)
            {
                return NotFound( new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                {
                    Content = new StringContent("No Team with this team code " + teamCode),
                    ReasonPhrase = "Team not Found"
                }
                );
            }
            return Ok();
        }

        [HttpGet]
        [Route("rooms")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetRoomsOfAuthUser()
        {
            return Ok(await userService.getAuthUserRooms(userService.GetUserId()));
        }
        
        
        [HttpGet]
        [Route("{username}")]
        public async Task<IActionResult> GetUserByUserName(string username)
        {
            return Ok(await userService.GetUserByUsername(username));
        }


        [HttpGet]
        [Route("projectmanagers/{teamId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetRoomsOfAuthUser(int teamId)
        {
            await userService.AddToProjectManagers(teamId,userService.GetUserId());
            return Ok();
        }

        [HttpGet]
        [Route("teams/{teamId}/leader/{username}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ChangeTeamLeaderByUsername(int teamId,string username)
        {
            Console.WriteLine("teamId = " + teamId + " , username = " + username);
            await userService.ChangeTeamLeader(teamId,userService.GetUserId(),username);
            return Ok();
        }

        [HttpGet]
        [Route("rooms/{roomId}/teams/leads")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetTeamsThatAuthUserLeadInARoom(int roomId)
        {
            return Ok(await userService.GetTeamsThatUserLeads(roomId,userService.GetUserId()));
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateUser(User user)
        {
            
            return Ok(await userService.UpdateUserInfo(userService.GetUserId(),user));
        }

        [HttpGet]
        [Route("authuser/teams/{teamId}/tasks")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetUserTasksInTeam(int teamId){
            return Ok(await taskService.GetTasksAssignedToUserInTeam(userService.GetUserId(),teamId));
        }
    }
}