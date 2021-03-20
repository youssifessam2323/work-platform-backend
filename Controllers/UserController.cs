using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using work_platform_backend.Dtos.Response;
using work_platform_backend.Models;
using work_platform_backend.Services;

namespace work_platform_backend.Controllers
{

    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly UserService userService ;
        private TeamService teamService { get; set; }



        public UserController(UserService userService, TeamService teamService )
        {
            this.userService = userService;
            this.teamService = teamService;
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
                return BadRequest(e.InnerException);
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
        
        

    }
}