using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;
using work_platform_backend.Services;

namespace work_platform_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly RoomService _roomService;
        private readonly TeamService _teamService;

        public RoomController(RoomService roomService , TeamService teamService)
        {
            _roomService = roomService;
            _teamService = teamService;
        }

        [HttpGet]
        [Route("GetRoom/{RoomId}")]
        public async Task<IActionResult> GetRoom(int RoomId)
        {

            var Room = await _roomService.GetRoom(RoomId);
            if (Room == null)
            {
                return NotFound();

            }
            return Ok(Room);

        }

        [HttpPost("AddRoom")]
        public async Task<IActionResult> AddRoom(Room room)
        {
            var NewRoom = await _roomService.AddRoom(room);
            if (NewRoom != null)
            {
                Team NewTeam = new Team()
                {
                    Name = $" {NewRoom.Name} / Owner ",
                    Description = NewRoom.Description,
                    RoomId = NewRoom.Id
                    
                };

               var NewTeamByDefault = await _teamService.AddTeam(NewTeam);
                return Ok(new { NewRoom, NewTeam });
            }
            return BadRequest();
        }

        [HttpPut("{RoomId}")]
        public async Task<IActionResult> UpdateRoom(int RoomId, Room room)
        {
            Room UpdatedRoom = await _roomService.UpdateRoom(RoomId, room);
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
                await _roomService.DeleteRoom(roomId);


            }
            catch (Exception Ex)
            {

                return NotFound(Ex.Message);
            }

            return Ok($"Object with id = {roomId} was  Deleted");
        }

    }
}
