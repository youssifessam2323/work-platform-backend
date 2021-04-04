using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using work_platform_backend.Dtos;
using work_platform_backend.Models;
using work_platform_backend.Services;

namespace work_platform_backend.Controllers
{
    [Route("api/v1/rooms")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly RoomService roomService;
        private readonly TeamService teamService;
        private readonly UserService UserService;
        private readonly ProjectManagerService projectManagerService;
        private readonly ProjectService projectService;

        public RoomController(RoomService roomService, TeamService teamService, UserService getUserService, ProjectManagerService projectManagerService, ProjectService projectService)
        {
            this.roomService = roomService;
            this.teamService = teamService;
            UserService = getUserService;
            this.projectManagerService = projectManagerService;
            this.projectService = projectService;
        }







        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetAllRooms()
        {

            var Room = await roomService.GetAllRooms();
            if (Room == null)
            {
                return NotFound();

            }
            return Ok(Room);

        }

        [HttpGet("{roomId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetRoom(int roomId)
        {

            var room = await roomService.GetRoom(roomId);
            if (room == null)
            {   
                return NotFound();

            }
            return Ok(room);

            
        }

        [HttpGet("{RoomId}/projects")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetRoomProjects(int roomId)
        {
           return Ok(await roomService.GetRoomProjects(roomId));
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> AddRoom(Room roomRequest)
        {
              
            var newRoom = await roomService.AddRoom(roomRequest,UserService.GetUserId());
            if(newRoom == null)
                return BadRequest();
                
                return Ok(newRoom);
        }

        [HttpPut("{RoomId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateRoom(int RoomId, Room room)
        {
            Room updatedRoom = await roomService.UpdateRoom(RoomId,room);
            if (updatedRoom == null)
            {
                return NotFound();
            }
            return Ok(updatedRoom);

        }


        [HttpDelete("{roomId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]

        public async Task<IActionResult> DeletRoom(int roomId)
        {
            try
            {
                await roomService.DeleteRoom(roomId);


            }
            catch (Exception Ex)
            {

                return NotFound(string.Format("there is no room with id = {0}",roomId));
            }

            return Ok($"Object with id = {roomId} was  Deleted");
        }

        
        [HttpGet("{roomId}/projectmanagers")]
        [Authorize(AuthenticationSchemes = "Bearer")]

        public async Task<IActionResult> GetProjectManagers(int roomId)
        {
            return Ok(await projectManagerService.GetProjectManagersByRoom(roomId));
        }

         
        [HttpGet("{roomId}/teams")]
        [Authorize(AuthenticationSchemes = "Bearer")]

        public async Task<IActionResult> GetTeamsInRoom(int roomId)
        {

            var GetTeamsRoom = await teamService.GetTeamsByRoom(roomId);
            if (GetTeamsRoom == null)
            {
                return Ok(new List<Team>());

            }
            return Ok(GetTeamsRoom);

        }

        [HttpPost("{roomId}/projects")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddNewProjectToRoom(int roomId,Project project)
        {
            return  Ok( await projectService.AddNewProjectToRoom(UserService.GetUserId(),roomId,project));
        }


    }
}
