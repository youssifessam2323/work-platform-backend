using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using work_platform_backend.Dtos;
using work_platform_backend.Exceptions;
using work_platform_backend.Exceptions.Team;
using work_platform_backend.Hubs;
using work_platform_backend.Services;

namespace work_platform_backend.Controllers
{

    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly UserService userService ;
        private readonly TaskService taskService;
        private readonly TeamService teamService;
        private readonly NotificationService notificationService;
        private readonly IHubContext<NotificationHub> notificationHub;
        private readonly TeamChatService teamChatService;
        private readonly IHubContext<ChatHub> chatHub;
        private readonly SessionService sessionService;
        private readonly ProjectManagerService projectManagerService;

        public UserController(UserService userService, TeamService teamService, TaskService taskService, NotificationService notificationService, IHubContext<NotificationHub> hub, TeamChatService teamChatService, IHubContext<ChatHub> chatHub, IHubContext<NotificationHub> notificationHub, SessionService sessionService, ProjectManagerService projectManagerService)
        {
            this.userService = userService;
            this.teamService = teamService;
            this.taskService = taskService;
            this.notificationService = notificationService;
            this.teamChatService = teamChatService;
            this.chatHub = chatHub;
            this.notificationHub = notificationHub;
            this.sessionService = sessionService;
            this.projectManagerService = projectManagerService;
        }




        /// <summary>
        /// Get the Current Authenticated user, used for development purpose 
        /// </summary>
        [HttpGet]
        [Route("current")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetAuthenticatiedUser()
        {
            try
            {
                var user =  await userService.getUserById(userService.GetUserId());
                return Ok(user);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
                
        }


        /// <summary>
        /// return a true if the username is already used 
        /// </summary>
        [HttpGet]
        [Route("username/{username}/exists")]
                public async Task<IActionResult> isUsernameExists(string username )
        {
            return Ok( await userService.IsUsernameExists(username));    
        }


        /// <summary>
        /// return a true if the username is already used 
        /// </summary>
        [HttpGet]
        [Route("email/{email}/exists")]
                public async Task<IActionResult> isEmailExists(string email )
        {
            return Ok( await userService.IsEmailExistsExists(email));    
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



        //  /// <summary>
        // ///  request user to join team (currently it make him join automatically, after adding notification feature we will alter this)
        // /// </summary>
        // [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        // [ProducesResponseType(typeof(void), (int)HttpStatusCode.Unauthorized)]     
        // [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]     
        // [HttpGet]
        // [Route("{userId}/join/teams/{teamCode}")] //{userId}/join/teams/{teamCode}
        // [Authorize(AuthenticationSchemes = "Bearer")]
        // public async Task<IActionResult> JoinTeam(string userId, string teamCode)
        // {
        //     try
        //     {
        //         var team = await teamService.GetTeamByTeamCode(teamCode);
        //         await userService.JoinTeam(teamCode,userId);
                
        //         var notification = await notificationService.CreateNewNotificaition(new Notification
        //         {
        //             Content = $"the leader of team {team.Name} approved your request and you have joined the team successfully",
        //             UserId = userId,
        //         });
        //             await notificationHub.Clients.User(userId).SendAsync("recievenotification",notification);                                    
        //     }
        //     catch(Exception e)
        //     {
        //         return NotFound(e.Message);
        //     }
        //     return Ok();
        // }


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



        
        [HttpPost]
        [Route("projectmanagers/{roomId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddUserToProjectManagers(int roomId, List<string> usernames)
        {  
            try
            {
                await projectManagerService.AddProjectManagers(roomId,usernames);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpDelete]
        [Route("projectmanagers/{roomId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> RemoveUserToProjectManagers(int roomId, List<string> usernames)
        {  
            try
            {
                await projectManagerService.RemoveFromProjectManagers(roomId,usernames);
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



        // [HttpGet("request/jointeam/{teamCode}")]
        [HttpGet("jointeam/{teamCode}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> RequestJoinTeam(string teamCode)
        {
            try
            {
               

                var userId = userService.GetUserId();

             
                
                await userService.JoinTeam(teamCode,userId);
                // var notification = new Notification
                // {
                //     Content = $"a new user {user.Name} wants to join the team {team.Name} that you lead",
                //     Url  = $"{this.Request.Host}/api/v1/users/{userId}/join/teams/{teamCode}",
                //     UserId = teamLeaderId
                // }; 

                // var newNotification = await notificationService.CreateNewNotificaition(notification);
                // Console.WriteLine("no id ===========================>" + newNotification.Id); 
                // await notificationHub.Clients.User(teamLeaderId).SendAsync("recievenotification",notification);                                    
                

                return Ok("Joined team successfully");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("task/{taskId}/sessions/open")]
        public async Task<IActionResult> OpenNewSession(int taskId)
        {
            try
            {
                int sessionId = await sessionService.OpenSession(taskId,userService.GetUserId());
                return Ok(sessionId);
            }
            catch(Exception e )
            {
                return BadRequest(e.Message);
            }
        }


        //TODO: not working 
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("task/sessions/{sessionId}/close")]
        public async Task<IActionResult> CloseSession(int sessionId,[FromQuery(Name = "extra-time")] int extraMinuties)
        {
            try
            {
                return Ok(await sessionService.CloseSession(sessionId, extraMinuties));
            }
            catch(Exception e )
            {
                return BadRequest(e.Message);
            }
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("task/sessions/current")]
        public async Task<IActionResult> GetCurrentSession()
        {
            try
            {    
                return Ok( await sessionService.GetCurrentSession(userService.GetUserId()));
            }
            catch(Exception e )
            {
                return BadRequest(e.Message);
            }
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("task/sessions")]
        public async Task<IActionResult> GetSessionInSpecificPeriod(
                                                            [FromQuery(Name = "start-date")]string startDateTime,
                                                            [FromQuery(Name = "end-date")]string endDateTime
                                                          )
        {
            try
            {    
                Console.WriteLine("start date =====> " + startDateTime + " end date ========> " + endDateTime );
                return Ok( await sessionService.GetSessionsBetweenTwoDates(userService.GetUserId(), startDateTime, endDateTime));
            }
            catch(Exception e )
            {
                return BadRequest(e.Message);
            }
        }
    }
}