using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using work_platform_backend.Dtos;
using work_platform_backend.Exceptions;
using work_platform_backend.Exceptions.Room;
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
        private readonly IMapper mapper;

        public RoomController(RoomService roomService, TeamService teamService, UserService getUserService, ProjectManagerService projectManagerService, ProjectService projectService, IMapper mapper)
        {
            this.roomService = roomService;
            this.teamService = teamService;
            UserService = getUserService;
            this.projectManagerService = projectManagerService;
            this.projectService = projectService;
            this.mapper = mapper;
        }






        ///<summary>
        /// Get All rooms in the system
        ///</summary>
        [ProducesResponseType(typeof(List<RoomDetailsDto>), (int)HttpStatusCode.OK)]
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetAllRooms()
        {

            var Room = await roomService.GetAllRooms();
            return Ok(Room);

        }


         ///<summary>
        /// Get a room by Id
        ///</summary>
        /// <param name="roomId"></param> 
        [HttpGet("{roomId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(RoomDetailsDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]    
        public async Task<IActionResult> GetRoomById(int roomId)
        {

            try{
            var roomResponse = await roomService.GetRoom(roomId);
            return Ok(roomResponse);
            }
            catch(RoomNotFoundException e)
            {
                return NotFound(e.Message);
            }
            
        }

        ///<summary>
        /// Get projects of this room
        ///</summary>
        ///<param name="roomId"></param>
        [ProducesResponseType(typeof(List<ProjectDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [HttpGet("{RoomId}/projects")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetRoomProjects(int roomId)
        {
            try
            {
                return Ok(await roomService.GetRoomProjects(roomId));
            }
            catch(RoomNotFoundException e)
            {  
               return NotFound(e.Message); 
            }
        }

        
        ///<summary>
        /// add new room (note : room name must be unique)
        ///</summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]    
        public async Task<IActionResult> AddRoom(RoomDto room)
        {
            try
            { 
                await roomService.AddRoom(room,UserService.GetUserId());
                return Ok("room created successfully");
            }
            catch(RoomNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch(RoomNameMustBeUniqueException e)
            {
                return BadRequest(e.Message);
            }
        }

        
        ///<summary>
        /// update existing room (note : room name must be unique)
        ///</summary>
        [HttpPut("{roomId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)] 
        public async Task<IActionResult> UpdateRoom(int roomId, RoomDto roomDto)
        {
            try
            {
                 await roomService.UpdateRoom(roomId,roomDto);
                return Ok("room updated successfully");
            }
            catch(RoomNotFoundException e)
            {
               return NotFound(e.Message);
            }
             catch(RoomNameMustBeUniqueException e)
            {
                return BadRequest(e.Message);
            }
            

        }




        ///<summary>
        /// delete room by Id
        ///</summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)] 
        [HttpDelete("{roomId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]

        public async Task<IActionResult> DeletRoom(int roomId)
        {
            try
            {
                await roomService.DeleteRoom(roomId);
            }
            catch (RoomNotFoundException e)
            {

                return NotFound(e.Message);
            }

            return Ok("Room was deleted successfully");
        }

        
        [HttpGet("{roomId}/projectmanagers")]
        [Authorize(AuthenticationSchemes = "Bearer")]

        public async Task<IActionResult> GetProjectManagers(int roomId)
        {
            try
            {
            return Ok(await projectManagerService.GetProjectManagersByRoom(roomId));
            }
            catch(RoomNotFoundException e)
            {
                return NotFound(e.Message);
            }
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
