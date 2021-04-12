using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using work_platform_backend.Dtos;
using work_platform_backend.Exceptions;
using work_platform_backend.Exceptions.Team;
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
        

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        [Route("creator/teams")]
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

        
     
        /// <summary>
        /// Get the Current Authenticated user, used for development purpose 
        /// </summary>
        [HttpGet]
        [Route("current")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetAuthenticatiedUser()
        {
            return Ok( await userService.getUserById(userService.GetUserId()));    
        }


        /// <summary>
        ///  get all  the teams of auth user in specific team
        /// </summary>
        [ProducesResponseType(typeof(List<TeamDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]     
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Unauthorized)]     
        [HttpGet]
        [Route("rooms/{roomId}/teams")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> TeamsOfAuthUserInRoom(int roomId)
        {   
            try
            {
            return Ok(await userService.getTeamOfUserInRoom(roomId,userService.GetUserId()));
            }
            catch(RoomNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }



         /// <summary>
        ///  request user to join team (currently it make him join automatically, after adding notification feature we will alter this)
        /// </summary>
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Unauthorized)]     
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]     
        [HttpGet]
        [Route("join/teams/{teamCode}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> JoinTeam(string teamCode)
        {
            try
            {
                await userService.JoinTeam(teamCode,userService.GetUserId());
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
            return Ok();
        }


        /// <summary>
        ///  returns the rooms of a the current auth user
        /// </summary>
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Unauthorized)]     
        [HttpGet]
        [Route("rooms")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetRoomsOfAuthUser()
        {
            return Ok(await userService.getAuthUserRooms(userService.GetUserId()));
        }
        
        /// <summary>
        ///  get a user by his username
        /// </summary>
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [HttpGet]
        [Route("{username}")]
        public async Task<IActionResult> GetUserByUserName(string username)
        {
            return Ok(await userService.GetUserByUsername(username));
        }



        ///<summary>
        /// Add new user to project managers(we using the teamId to detect the room)
        ///(Will be Change to notify the Room Owner to approve the request ) 
        ///</summary>
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Unauthorized)]
        [HttpGet]
        [Route("projectmanagers/{teamId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddUserToProjectManagers(int teamId)
        {  
            try
            {
                await userService.AddToProjectManagers(teamId,userService.GetUserId());
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        ///<summary>
        ///  change the team leader by username (later will be altered)
        ///</summary>
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [HttpGet]
        [Route("teams/{teamId}/leader/{username}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ChangeTeamLeaderByUsername(int teamId,string username)
        {
            try
            {
            await userService.ChangeTeamLeader(teamId,userService.GetUserId(),username);
            return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.InnerException);
            }
        }


        ///<summary>
        ///  change the team leader by username (later will be altered)
        ///</summary>
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [HttpGet]
        [Route("rooms/{roomId}/teams/leads")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetTeamsThatAuthUserLeadInARoom(int roomId)
        {
            try
            {
            return Ok(await userService.GetTeamsThatUserLeads(roomId,userService.GetUserId()));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        

         ///<summary>
        ///  update the current authenticated service
        ///</summary>
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateUser(UserDto userDto)
        {
            try
            {
                await userService.UpdateUserInfo(userService.GetUserId(),userDto);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        ///<summary>
        ///Get user tasks in this team
        ///</summary>
        [HttpGet]
        [Route("authuser/teams/{teamId}/tasks")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetUserTasksInTeam(int teamId){
            try
            {
            return Ok(await taskService.GetTasksAssignedToUserInTeam(userService.GetUserId(),teamId));
            }
            catch(TeamNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        
        
        //test b3deen

        ///<summary>
        /// get all the task created by the auth user
        ///</summary>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("creator/tasks")]
        public async Task<IActionResult> GetTasksOfCreator()
        {
            var TaskByCreator = await taskService.GetTaskByCreator(userService.GetUserId());
            return Ok(TaskByCreator);

        }
    }
}