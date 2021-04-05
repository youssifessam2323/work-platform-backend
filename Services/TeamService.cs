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
        private readonly ITeamRepository teamRepository;
        private readonly IMapper mapper;
        private readonly IProjectRepository projectRepository;
        private readonly IRTaskRepository taskRepository;
        private readonly TeamChatService teamChatService;

        public TeamService(ITeamRepository teamRepository, IMapper mapper, IProjectRepository projectRepository, IRTaskRepository taskRepository , TeamChatService teamChatService)
        {
            this.teamRepository = teamRepository;
            this.mapper = mapper;
            this.projectRepository = projectRepository;
            this.taskRepository = taskRepository;
            this.teamChatService = teamChatService;
        }


        public async Task<Team> AddTeam(Team newTeam,int roomId,string creatorId)
        {

            if (newTeam != null)
            {
                newTeam.RoomId = roomId;
                newTeam.LeaderId = creatorId;
                newTeam.CreatedAt = DateTime.Now;
                newTeam.TeamCode = Guid.NewGuid();
                await teamRepository.SaveTeam(newTeam);
                await teamRepository.SaveChanges();

                TeamChat newTeamChat = new TeamChat()
                {
                    ChatName = $" {newTeam.Name}/Chatting ",
                };

                await teamChatService.CreateTeamChat(newTeamChat, newTeam.LeaderId, newTeam.Id);

                return newTeam;
            }
            return null;

        }

        public async Task<Team> UpdateTeam(int id, Team Team)
        {
            Team NewTeam = await teamRepository.UpdateTeamById(id,Team);

            if (NewTeam != null)
            {
               await teamRepository.SaveChanges();
                return NewTeam;
            }


            return null;

        }


        public async Task DeleteTeam(int teamId)
        {
            var Subteams = await teamRepository.GetTeamSubTeamsById(teamId);
            var team = await teamRepository.DeleteTeamById(teamId);
            if (team == null)
            {

                throw new NullReferenceException();

            }
           

            await teamRepository.SaveChanges();


        }


  

        public async Task<IEnumerable<Team>> GetTeamsByCreator(string CreatorId)
        {
            var Teams = await teamRepository.GetAllTeamsByCreator(CreatorId);

            if (Teams.Count().Equals(0))
            {
                return null;

            }

            return Teams;

        }

        public async Task<IEnumerable<Team>> GetTeamsByMember(string UserId)
        {
            var Teams = await teamRepository.GetAllTeamsByMember(UserId);


            return Teams;

        }

        public async Task<IEnumerable<Team>> GetTeamsByRoom(int RoomId)
        {
            var Teams = await teamRepository.GetAllTeamsByRoom(RoomId);

            if (Teams.Count().Equals(0))
            {

                return new List<Team>();
                
            }
            var TeamResponse = mapper.Map<IEnumerable<Team>>(Teams);
            



            return TeamResponse;

        }

        public async Task<List<User>> GetMembersOfTeam(int teamId)
        {
            return await teamRepository.GetMembersOfTeam(teamId);
        }

     
        public async Task<List<Team>> GetTeamSubTeams(int teamId)
        {
            return await teamRepository.GetTeamSubTeamsById(teamId);
        }

        public async Task<List<Project>> GetProjectsOfTeam(int teamId)
        {   
            return await projectRepository.GetProjectByTeam(teamId);
        }

        public async Task<List<RTask>> GetTasksOfTeam(int teamId)
        {
                return await taskRepository.GetTasksByTeam(teamId);
        }

        public async Task<Team> GetTeam(int TeamId)
        {
            var Teams = await teamRepository.GetTeamById(TeamId);

            if (Teams==null)
            {
                return null;

            }

            return Teams;

        }


        public async Task<Team> GetTeamByTeamCode(string teamCode)
        {
            var team = await teamRepository.GetTeamByTeamCode(teamCode);

            if (team == null)
            {
                return null;

            }

            return team;

        }




    }
}

