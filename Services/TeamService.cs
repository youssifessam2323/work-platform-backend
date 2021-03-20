using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Dtos;
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class TeamService
    {
        private readonly ITeamRepository _TeamRepo;
        private readonly IMapper _mapper;

        public TeamService(ITeamRepository teamRepository,IMapper mapper)
        {
            _TeamRepo = teamRepository;
            _mapper = mapper;
          
        }


        public async Task<Team> AddTeam(Team newTeam,int roomId,string creatorId)
        {
            if (newTeam != null)
            {
                newTeam.RoomId = roomId;
                newTeam.LeaderId = creatorId;
                newTeam.CreatedAt = DateTime.Now;
                newTeam.TeamCode = Guid.NewGuid();
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


            return Teams;

        }

        public async Task<IEnumerable<ResponseTeamDto>> GetTeamsByRoom(int RoomId)
        {
            var Teams = await _TeamRepo.GetAllTeamsByRoom(RoomId);

            if (Teams.Count().Equals(0))
            {

                return null;



                
            }
            var TeamResponse = _mapper.Map<IEnumerable<ResponseTeamDto>>(Teams);
            



            return TeamResponse;

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

