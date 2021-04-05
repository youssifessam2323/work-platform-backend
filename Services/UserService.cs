using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
        
                

        private IMapper mapper;

        public UserService(IHttpContextAccessor HttpContextAccessor, IUserRepository userRepository, ITeamRepository teamRepository = null, IMapper mapper = null, IRoomRepository roomRepository = null, ProjectManagerService projectManagerService = null)
        {
            this.HttpContextAccessor = HttpContextAccessor;
            this.userRepository = userRepository;
            this.teamRepository = teamRepository;
            this.mapper = mapper;
            this.roomRepository = roomRepository;
            this.projectManagerService = projectManagerService;
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

        public async Task<List<Team>> getTeamOfUserInRoom(int roomId,string userId)
        {
            Console.WriteLine("Room ID =====> = " +  roomId);
            List<Team> teams =((List<Team>) await teamRepository.GetAllTeamsByMember(userId)).Where(t => t.RoomId == roomId).ToList();

            return teams;
        }


        //Complete from here 
        public async Task JoinTeam(string teamCode,string userId)
        {
            Team team = await teamRepository.GetTeamByTeamCode(teamCode);
            User user = await userRepository.GetUserById(userId);     
            
            Console.WriteLine("Team ====> " + team);
            Console.WriteLine("User ====> " + user);
            if(team == null)
            {
                throw new ApplicationException();
            }
            await userRepository.SaveNewTeamMember(user,team);     
            await userRepository.SaveChanges();   
        }

        public async Task LeaveTeam(int teamId, string userId)
        {
            Team team = await teamRepository.GetTeamById(teamId);
            User user = await userRepository.GetUserById(userId);

            Console.WriteLine("Team ====> " + team);
            Console.WriteLine("User ====> " + user);
            if (team == null )
            {
                throw new ApplicationException();
            }
             userRepository.DeleteTeamMember(user, team);
            await userRepository.SaveChanges();
        }

        public async Task ChangeTeamLeader(int teamId, string userId,string newLeaderUsername)
        {
            Team team = await teamRepository.GetTeamById(teamId);

            if(team.LeaderId != userId)
            {
                throw new UnauthorizedAccessException("This Operation is For the Team Leader Only");
            }

            User user = await userRepository.GetUserByUsername(newLeaderUsername);
            
            team.Leader = user ;
            await teamRepository.SaveChanges();
        }

        public async Task<List<Team>> GetTeamsThatUserLeads(int roomId, string userId)
        {
            var teams = await teamRepository.GetAllTeamsByRoom(roomId);
            return teams.Where(t => t.LeaderId == userId).ToList();
        }

        public async Task<User> UpdateUserInfo(string userId, User newUser)
        {   
            return await userRepository.UpdateUser(userId,newUser);
        }

        public async Task AddToProjectManagers(int teamId, string userId)
        {
            Team team = await teamRepository.GetTeamById(teamId);
            if(team != null)
            {
                Room room = await roomRepository.GetRoomById(team.RoomId); 
                User user = await userRepository.GetUserById(userId);
                await projectManagerService.AddNewProjectManager(user,room);
            }else
            {
            throw new NullReferenceException("team Id = " + teamId + " is not found");
            }
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await userRepository.GetUserByUsername(username);

        }

        public async Task<HashSet<Room>> getAuthUserRooms(string userId)
        {
            Console.WriteLine("Authenticad User = " + userId);
            List<Team> teams =await userRepository.getUserTeams(userId);

            HashSet<Room> rooms = new HashSet<Room>();

            foreach(var team in teams)
            {
                rooms.Add(await roomRepository.GetRoomById(team.RoomId));
            }    
            return rooms;
        }
    }
}
