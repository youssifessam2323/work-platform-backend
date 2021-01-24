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
    public class TeamController : ControllerBase
    {
        private readonly TeamService _teamService;

        public TeamController(TeamService teamService)
        {
            _teamService = teamService;
        }


        [HttpGet]
        [Route("GetTeamsCreator/{TeamCreatorId}")]
        public async Task<IActionResult> GetTeamsCreator(string TeamCreatorId)
        {

            var GetTeamsByCreator = await _teamService.GetTeamsByCreator(TeamCreatorId);
            if (GetTeamsByCreator == null)
            {
                return NotFound();

            }
            return Ok(GetTeamsByCreator);

        }


        [HttpGet]
        [Route("GetTeamsMember/{userId}")]

        public async Task<IActionResult> GetTeamsOfMember(string userId)
        {

            var GetTeamsMember = await _teamService.GetTeamsByMember(userId);
            if (GetTeamsMember == null)
            {
                return NotFound();

            }
            return Ok(GetTeamsMember);

        }

        [HttpGet]
        [Route("GetTeamsRoom/{roomId}")]
        public async Task<IActionResult> GetTeamsInRoom(int roomId)
        {

            var GetTeamsRoom = await _teamService.GetTeamsByRoom(roomId);
            if (GetTeamsRoom == null)
            {
                return NotFound();

            }
            return Ok(GetTeamsRoom);

        }

        [HttpGet]
        [Route("GetTeam/{teamId}")]
        public async Task<IActionResult> GetSingleTeam(int teamId)
        {

            var Team = await _teamService.GetTeam(teamId);
            if (Team == null)
            {
                return NotFound();

            }
            return Ok(Team);

        }

        [HttpPost("AddTeam")]
        public async Task<IActionResult> AddTeam(Team team)
        {
            var NewTeam = await _teamService.AddTeam(team);
            if (NewTeam != null)
            {
               return Ok(NewTeam);
            }
            return BadRequest();
        }

        [HttpPut("{TeamId}")]
        public async Task<IActionResult> UpdateTeam(int TeamId, Team team)
        {
            Team UpdatedTeam = await _teamService.UpdateTeam(TeamId, team);
            if (UpdatedTeam == null)
            {
                return NotFound();
            }
            return Ok(UpdatedTeam);

        }


        [HttpDelete]
        [Route("delete/{teamId}")]
       public async Task<IActionResult> DeletTeam(int teamId)
        {
            try
            {
                await _teamService.DeleteTeam(teamId);


            }
            catch (Exception Ex)
             {

                return NotFound(Ex.Message);
            }

           return Ok($"Object with id = {teamId} was  Deleted");
        }

    }




}

