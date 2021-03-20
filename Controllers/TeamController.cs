using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using work_platform_backend.Models;
using work_platform_backend.Services;

namespace work_platform_backend.Controllers
{
    [Route("api/v1/teams")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly TeamService _teamService;
        private readonly UserService UserService;

        public TeamController(TeamService teamService,UserService getuserService)
        {
            _teamService = teamService;
            UserService = getuserService;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        [Route("GetTeamsCreator")]
        public async Task<IActionResult> GetTeamsCreator()
        {
          string TeamCreatorId = UserService.GetUserId();

            var GetTeamsByCreator = await _teamService.GetTeamsByCreator(TeamCreatorId);
            if (GetTeamsByCreator == null)
            {
                return Ok(new List<Team>());

            }
            return Ok(GetTeamsByCreator);

        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        [Route("GetTeamsMember")]

        public async Task<IActionResult> GetTeamsOfMember()
        {
            string TeamMemberId = UserService.GetUserId();
            var GetTeamsMember = await _teamService.GetTeamsByMember(TeamMemberId);
            if (GetTeamsMember == null)
            {
                return Ok(new List<Team>());


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
                return Ok(new List<Team>());

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


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("AddTeam/{roomId}")]
        public async Task<IActionResult> AddTeam(Team team,int roomId)
        {
            var NewTeam = await _teamService.AddTeam(team,roomId, UserService.GetUserId());
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

