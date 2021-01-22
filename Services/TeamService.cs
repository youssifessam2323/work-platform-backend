using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class TeamService
    {
        private readonly ITeamRepository _TeamRepo;
        

        public TeamService(ITeamRepository teamRepository )
        {
            _TeamRepo = teamRepository;
          
        }


        public async Task<Team> AddTeam(Team newTeam)
        {
            if (newTeam != null)
            {
                await _TeamRepo.SaveTeam(newTeam);
                await _TeamRepo.SaveChanges();
                return newTeam;
            }
            return null;

        }

        public async Task<Team> UpdateTeam(int id, Team Team)
        {
            Team NewTeam = await _TeamRepo.UpdateTeamById(id,Team);

            if (NewTeam != null)
            {
               await _TeamRepo.SaveChanges();
                return NewTeam;
            }


            return null;

        }


        public async Task DeleteTeam(int teamId)
        {
            var Team = await _TeamRepo.DeleteTeamById(teamId);
            if (Team == null)
            {

                throw new NullReferenceException();

            }

            await _TeamRepo.SaveChanges();


        }


  

        public async Task<IEnumerable<Team>> GetTeamsByCreator(string CreatorId)
        {
            var Teams = await _TeamRepo.GetAllTeamsByCreator(CreatorId);

            if (Teams.Count().Equals(0))
            {
                return null;

            }

            return Teams;

        }

        public async Task<IEnumerable<Team>> GetTeamsByMember(string UserId)
        {
            var Teams = await _TeamRepo.GetAllTeamsByMember(UserId);

            if (Teams.Count().Equals(0))
            {
                return null;

            }

            return Teams;

        }

        public async Task<IEnumerable<Team>> GetTeamsByRoom(int RoomId)
        {
            var Teams = await _TeamRepo.GetAllTeamsByRoom(RoomId);

            if (Teams.Count().Equals(0))
            {
                return null;

            }

            return Teams;

        }


        public async Task<Team> GetTeam(int TeamId)
        {
            var Teams = await _TeamRepo.GetTeamById(TeamId);

            if (Teams==null)
            {
                return null;

            }

            return Teams;

        }




    }
}

