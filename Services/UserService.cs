using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using work_platform_backend.Dtos.Response;
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
        
                

        private IMapper mapper;



        public UserService(IHttpContextAccessor HttpContextAccessor, IUserRepository userRepository, ITeamRepository teamRepository = null, IMapper mapper = null, IRoomRepository roomRepository = null)
        {
            this.HttpContextAccessor = HttpContextAccessor;
            this.userRepository = userRepository;
            this.teamRepository = teamRepository;
            this.mapper = mapper;
            this.roomRepository = roomRepository;
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

        public async Task<UserResponse> getUserById(string id)
        {
            var user = await userRepository.GetUserById(id);
             return mapper.Map<UserResponse>(user);
        }

        public async Task<List<TeamResponse>> getTeamOfUserInRoom(int roomId,string userId)
        {
            Console.WriteLine("Room ID =====> = " +  roomId);
            List<Team> teams =((List<Team>) await teamRepository.GetAllTeamsByMember(userId)).Where(t => t.RoomId == roomId).ToList();

            List<TeamResponse> teamResponse =teams.Select(t => mapper.Map<TeamResponse>(t)).ToList();
            return teamResponse;
        }


        //Complete from here 
        public async Task JoinTeam(string teamCode,string userId)
        {
            Team team = await teamRepository.GetTeamByTeamCode(teamCode);
            User user = await userRepository.GetUserById(userId);     
            
            Console.WriteLine("Team ====> " + team);
            Console.WriteLine("User ====> " + user);
            
            await userRepository.SaveNewTeamMember(user,team);     
            await userRepository.SaveChanges();   
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
