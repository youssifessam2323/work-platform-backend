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
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly RoomService RoomService;
        private readonly TeamService TeamService;
        private readonly UserService UserService;

        public RoomController(RoomService roomService, TeamService teamService, UserService getUserService)
        {
            RoomService = roomService;
            TeamService = teamService;
            UserService = getUserService;



        }


       
  



        [HttpGet]
        [Route("GetAllRoom")]
        public async Task<IActionResult> GetAllRooms()
        {

            var Room = await RoomService.GetAllRooms();
            if (Room == null)
            {
                return NotFound();

            }
            return Ok(Room);

        }

        [HttpGet]
        [Route("GetRoom/{RoomId}")]
        public async Task<IActionResult> GetRoom(int RoomId)
        {

            var Room = await RoomService.GetRoom(RoomId);
            if (Room == null)
            {
                return NotFound();

            }
            return Ok(Room);

            
        }

        [Authorize(AuthenticationSchemes="Bearer")]
        [HttpGet]
        [Route("GetAllRoomOfCreator")]
        public async Task<IActionResult> GetRoomsOfCreator()
        {
            string RoomCreatorId = UserService.GetUserId();



            var Room = await RoomService.GetAllRoomsOfCreator(RoomCreatorId);
            if (Room== null)
            {
                
                return Ok(new List<Room>());

            }
            return Ok(Room);

        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        [Route("GetAllRoomOfUser")]
        public async Task<IActionResult> GetAllRoomOfUser()
        {
            string memberId = UserService.GetUserId();

            var TeamsOfUser = await TeamService.GetTeamsByMember(memberId);

            if (TeamsOfUser !=null)
            {
                List<ResponseRoomDto> RoomOfTeams = new List<ResponseRoomDto>();
                foreach (Team t in TeamsOfUser)
                {
                    RoomOfTeams.Add(await RoomService.GetRoom(t.RoomId));
                    
                }

                var RoomCreatedByMe = await RoomService.GetAllRoomsOfCreator(memberId); 

                foreach(var RoomDto in RoomCreatedByMe)
                {
                    RoomOfTeams.Add(RoomDto);
                }

                return Ok(RoomOfTeams.GroupBy(R => R.Id).Select(r => r.First()).ToList());

            }

            return Ok(new List<Room>()); 
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("AddRoom")]
        public async Task<IActionResult> AddRoom(RequestRoomDto requestRoomDto)
        {
            string creatorId = UserService.GetUserId();
            var NewRoom = await RoomService.AddRoom(requestRoomDto, creatorId);
            if (NewRoom != null)
            {
                
          
                Team NewTeam = new Team()
                {
                    Name = $" {NewRoom.Name}/main ",
                    Description = NewRoom.Description,
                    CreatedAt = DateTime.Now
                };

               var NewTeamByDefault = await TeamService.AddTeam(NewTeam,NewRoom.Id, creatorId);
                return Ok(new { NewRoom, NewTeam });
            }
            return BadRequest();
        }

        [HttpPut("{RoomId}")]
        public async Task<IActionResult> UpdateRoom(int RoomId, RequestRoomDto room)
        {
            Room UpdatedRoom = await RoomService.UpdateRoom(RoomId, room);
            if (UpdatedRoom == null)
            {
                return NotFound();
            }
            return Ok(UpdatedRoom);

        }


        [HttpDelete]
        [Route("deleteRoom/{roomId}")]
        public async Task<IActionResult> DeletRoom(int roomId)
        {
            try
            {
                await RoomService.DeleteRoom(roomId);


            }
            catch (Exception Ex)
            {

                return NotFound(Ex.Message);
            }

            return Ok($"Object with id = {roomId} was  Deleted");
        }

    }
}
