using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using work_platform_backend.Dtos;
using work_platform_backend.Exceptions;
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public  class UserService
    {
        private readonly IHttpContextAccessor HttpContextAccessor;
        private readonly IUserRepository userRepository;
        private readonly ITeamRepository teamRepository;
        private readonly IRoomRepository roomRepository;
        private readonly ProjectManagerService projectManagerService;
        private readonly ITeamMembersRepository teamMembersRepository;
        
                

        private IMapper mapper;

        public UserService(IHttpContextAccessor HttpContextAccessor, IUserRepository userRepository, ITeamRepository teamRepository = null, IMapper mapper = null, IRoomRepository roomRepository = null, ProjectManagerService projectManagerService = null, ITeamMembersRepository teamMembersRepository = null)
        {
            this.HttpContextAccessor = HttpContextAccessor;
            this.userRepository = userRepository;
            this.teamRepository = teamRepository;
            this.mapper = mapper;
            this.roomRepository = roomRepository;
            this.projectManagerService = projectManagerService;
            this.teamMembersRepository = teamMembersRepository;
        }


        public  string GetUserId()
        {
            var UserIdClaim = HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);


            if (UserIdClaim !=null)
            {
                return UserIdClaim;
            }

            return null;
        }

        public async Task<User> getUserById(string id)
        {
            return await userRepository.GetUserById(id);
        }

        public async Task<List<TeamDto>> getTeamOfUserInRoom(int roomId,string userId)
        {
            Room room = await roomRepository.GetRoomById(roomId);
            if(room == null)
            {
                throw new RoomNotFoundException("room not exist");
            }
            
            List<Team> teams =((List<Team>) await teamRepository.GetAllTeamsByMember(userId)).Where(t => t.RoomId == roomId).ToList();
            
            var teamsDto = teams.Select(t => mapper.Map<TeamDto>(t)).ToList();
            
            return teamsDto;
        }

        public async Task<bool> IsUsernameExists(string username)
        {
            return await userRepository.IsUserExistByUsername(username); 
        }

        internal async Task<bool> IsEmailExistsExists(string username)
        {
            return await userRepository.IsUserExistByEmail(username); 
        }


        //Complete from here 
        public async Task JoinTeam(string teamCode,string userId)
        {
            Team team = await teamRepository.GetTeamByTeamCode(teamCode);
            User user = await userRepository.GetUserById(userId);     
            
            if(team == null)
            {
                throw new Exception("team not exist");
            }

            var teamMembers = await teamMembersRepository.GetTeamMembersByUserIdAndTeamId(userId,team.Id);
            
          
            Console.WriteLine("TeamMember  ====> " + teamMembers);
            if(teamMembers != null)
            {
                throw new Exception("user is aready in the team");
            }

            Console.WriteLine("Team ====> " + team);
            Console.WriteLine("User ====> " + user);
            
            await userRepository.SaveNewTeamMember(userId,team.Id);     
            await userRepository.SaveChanges();   
        }

        public async Task LeaveTeam(int teamId, string userId)
        {
            Team team = await teamRepository.GetTeamById(teamId);

            Console.WriteLine("Team ====> " + teamId);
            Console.WriteLine("User ====> " + userId);
            if (team == null )
            {
                throw new ApplicationException();
            }
            await userRepository.DeleteTeamMember(userId, teamId);
            await userRepository.SaveChanges();    
        }
        public async Task ChangeTeamLeader(int teamId, string userId,string newLeaderUsername)
        {
        
            Team team = await teamRepository.GetTeamById(teamId);

            var isAuthUserIsLeader = team.LeaderId == userId ? true : false;

            if(!isAuthUserIsLeader)
            {
                throw new UnauthorizedAccessException("this operation is for the leader only");
            }

            User user = await userRepository.GetUserByUsername(newLeaderUsername);
            
            
            team.LeaderId = user.Id ;
            await teamRepository.SaveChanges();
        }

        public async Task<List<TeamDto>> GetTeamsThatUserLeads(int roomId, string userId)
        {
            var isRoomExist = await roomRepository.isRoomExist(roomId);
            
            if(!isRoomExist)
            {
                throw new Exception("room not exist");
            }
            var teams = await teamRepository.GetAllTeamsByRoom(roomId);
            return teams.Where(t => t.LeaderId == userId).Select(t => mapper.Map<TeamDto>(t)).ToList();
        }

        public async Task UpdateUserInfo(string userId, UserDto userDto)
        {   
            var isUserExist = await userRepository.IsUserExistByUsername(userDto.UserName);

            if(isUserExist)
            {
                throw new Exception("username was taken");
            }
            var user = mapper.Map<User>(userDto);

            await userRepository.UpdateUser(userId,user);
        }




        public async Task AddToProjectManagers(int teamId, string userId)
        {
            Team team = await teamRepository.GetTeamById(teamId);
            
            if(team == null)
            {
                throw new Exception("team not exist");
            }


            var teamMembers = await teamMembersRepository.GetTeamMembersByUserIdAndTeamId(userId,teamId);
            if(teamMembers == null)
            {
                throw new Exception("user is not in this team");
            }

            Room room = await roomRepository.GetRoomById(team.RoomId); 


            var isProjectManagerExist = await  projectManagerService.IsProjectManagerExists(userId,room.Id);
            if(isProjectManagerExist)
            {
                throw new Exception("user is already a project manager in this room");
            }

            // User user = await userRepository.GetUserById(userId);
            await projectManagerService.AddNewProjectManager(userId,room.Id);

           
          
        }



        public async Task<User> GetUserByUsername(string username)
        {
            return await userRepository.GetUserByUsername(username);

        }

        public async Task<HashSet<RoomDto>> getAuthUserRooms(string userId)
        {
            Console.WriteLine("Authenticad User = " + userId);
            List<Team> teams =await userRepository.getUserTeams(userId);

            var creatorTeams =  await teamRepository.GetAllTeamsByCreator(userId);
            
            foreach(var t in creatorTeams)
            {
                teams.Add(t);
            }

            HashSet<Room> rooms = new HashSet<Room>();

            
            
            foreach(var team in teams)
            {
                rooms.Add(await roomRepository.GetRoomById(team.RoomId));
            }
            
            return rooms.Select(r => mapper.Map<RoomDto>(r)).ToHashSet();
             
        }
    }
}
