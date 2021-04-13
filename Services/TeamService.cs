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
        private readonly IRoomRepository roomRepository;
        private readonly ITeamMembersRepository teamMembersRepository;
        private readonly TeamChatService teamChatService;

        public TeamService(ITeamRepository teamRepository, IMapper mapper, IProjectRepository projectRepository, IRTaskRepository taskRepository, TeamChatService teamChatService, IRoomRepository roomRepository, ITeamMembersRepository teamMembersRepository)
        {
            this.teamRepository = teamRepository;
            this.mapper = mapper;
            this.projectRepository = projectRepository;
            this.taskRepository = taskRepository;
            this.teamChatService = teamChatService;
            this.roomRepository = roomRepository;
            this.teamMembersRepository = teamMembersRepository;
        }


        public async Task<Team> AddTeam(Team newTeam,int roomId,string creatorId)
        {
            if(! await roomRepository.IsRoomExist(roomId))
            {
                throw new Exception("room not exist");
            }

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
                    ChatName = $" {newTeam.Name}/GroupChat ",
                };

                await teamChatService.CreateTeamChat(newTeamChat, newTeam.LeaderId, newTeam.Id);

                return newTeam;
            }
            return null;

        }

        public async Task<Team> UpdateTeam(int id, Team Team)
        {
            if(! await teamRepository.IsTeamExist(id))
            {
                throw new Exception("team not exist");
            }
            Team NewTeam = await teamRepository.UpdateTeamById(id,Team);

            if (NewTeam != null)
            {
               await teamRepository.SaveChanges();
                return NewTeam;
            }
            throw new Exception("something goes wrong, please try again");
        }


        public async Task DeleteTeam(int teamId)
        {
            var Subteams = await teamRepository.GetTeamSubTeamsById(teamId);
            var team = await teamRepository.DeleteTeamById(teamId);
            if (team == null)
            {

                throw new NullReferenceException();

            }

          var teamProjects =  await projectRepository.GetProjectByTeam(teamId);

            if(teamProjects.Count()!=0)
            {
                foreach (Project project in teamProjects)    //Delete Team From TeamProjects
                {
                    await projectRepository.RemoveTeamFromProject(project.Id, teamId);
                }

               await projectRepository.SaveChanges();

            }

            await teamChatService.DeleteTeamChatByTeam(teamId);
            var teamMembers =  await teamMembersRepository.DeleteTeamsMembersByTeam(teamId);
            var rTask =  await taskRepository.DeleteTaskByTeam(teamId);
            
            if(teamMembers.Count()!=0)
            {
                await teamMembersRepository.SaveChanges();
            }

            if(rTask.Count() != 0)
            {
               await taskRepository.SaveChanges();
            }

            await teamRepository.SaveChanges();



        }

        public async Task DeleteTeamByRoom(int roomId)
        {
            var team = await teamRepository.DeleteTeamByRoom(roomId);
            if (team == null)
            {

                throw new NullReferenceException();

            }

           await teamRepository.SaveChanges();

            ICollection <TeamsMembers> teamsMembers  = new List<TeamsMembers>();
        
            ICollection<RTask> rTasks = new List<RTask>();
            foreach (Team t in team)
            {
                 await teamChatService.DeleteTeamChatByTeam(t.Id);
                teamsMembers = await teamMembersRepository.DeleteTeamsMembersByTeam(t.Id);
                 rTasks = await taskRepository.DeleteTaskByTeam(t.Id);
            }


            if (teamsMembers .Count()!=0)
            {
                await teamMembersRepository.SaveChanges();
            }

            if (rTasks.Count() != 0)
            {
                await taskRepository.SaveChanges();
            }

        }


            public async Task<IEnumerable<TeamDto>> GetTeamsByCreator(string CreatorId)
        {
            var teams = await teamRepository.GetAllTeamsByCreator(CreatorId);

            return teams.Select(t => mapper.Map<TeamDto>(t)).ToList();

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

        public async Task<List<UserDto>> GetMembersOfTeam(int teamId)
        {
            if(! await teamRepository.IsTeamExist(teamId))
            {
                throw new Exception("team not exist");
            }
            var members =  await teamRepository.GetMembersOfTeam(teamId);
       
            return members.Select(u => mapper.Map<UserDto>(u)).ToList();
        }

     
        public async Task<List<TeamDto>> GetTeamSubTeams(int teamId)
        {
            if(! await teamRepository.IsTeamExist(teamId))
            {
                throw new Exception("team not exist");
            }
            var subteams = await teamRepository.GetTeamSubTeamsById(teamId);

            return subteams.Select(t => mapper.Map<TeamDto>(t)).ToList();
        }

        public async Task<List<ProjectDto>> GetProjectsOfTeam(int teamId)
        {   
            if(! await teamRepository.IsTeamExist(teamId))
            {
                throw new Exception("team not exist");
            }

            var projects = await projectRepository.GetProjectByTeam(teamId);

            return projects.Select(p => mapper.Map<ProjectDto>(p)).ToList();
        }

        public async Task<List<TaskDto>> GetTasksOfTeam(int teamId)
        {
            if(! await teamRepository.IsTeamExist(teamId))
            {
                throw new Exception("team not exist");
            }

            var tasks =  await taskRepository.GetTasksByTeam(teamId);

            return tasks.Select(t => mapper.Map<TaskDto>(t)).ToList();
        }

        public async Task<TeamDetailsDto> GetTeam(int TeamId)
        {
            var team = await teamRepository.GetTeamById(TeamId);

            if (team== null)
            {
                throw new Exception("team not exist");
            }


            return mapper.Map<TeamDetailsDto>(team);

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

