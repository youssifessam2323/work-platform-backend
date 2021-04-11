using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
<<<<<<< HEAD
=======
using work_platform_backend.Dtos;
using work_platform_backend.Exceptions;
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
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
<<<<<<< HEAD
=======
        private readonly ITeamMembersRepository teamMembersRepository;
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
        
                

        private IMapper mapper;

<<<<<<< HEAD
        public UserService(IHttpContextAccessor HttpContextAccessor, IUserRepository userRepository, ITeamRepository teamRepository = null, IMapper mapper = null, IRoomRepository roomRepository = null, ProjectManagerService projectManagerService = null)
=======
        public UserService(IHttpContextAccessor HttpContextAccessor, IUserRepository userRepository, ITeamRepository teamRepository = null, IMapper mapper = null, IRoomRepository roomRepository = null, ProjectManagerService projectManagerService = null, ITeamMembersRepository teamMembersRepository = null)
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
        {
            this.HttpContextAccessor = HttpContextAccessor;
            this.userRepository = userRepository;
            this.teamRepository = teamRepository;
            this.mapper = mapper;
            this.roomRepository = roomRepository;
            this.projectManagerService = projectManagerService;
<<<<<<< HEAD
=======
            this.teamMembersRepository = teamMembersRepository;
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
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

<<<<<<< HEAD
        public async Task<List<Team>> getTeamOfUserInRoom(int roomId,string userId)
        {
            Console.WriteLine("Room ID =====> = " +  roomId);
            List<Team> teams =((List<Team>) await teamRepository.GetAllTeamsByMember(userId)).Where(t => t.RoomId == roomId).ToList();

            return teams;
=======
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
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
        }


        //Complete from here 
        public async Task JoinTeam(string teamCode,string userId)
        {
            Team team = await teamRepository.GetTeamByTeamCode(teamCode);
            User user = await userRepository.GetUserById(userId);     
            
<<<<<<< HEAD
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
=======
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
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
        }

        public async Task ChangeTeamLeader(int teamId, string userId,string newLeaderUsername)
        {
<<<<<<< HEAD
            Team team = await teamRepository.GetTeamById(teamId);

            if(team.LeaderId != userId)
            {
                throw new UnauthorizedAccessException("This Operation is For the Team Leader Only");
=======
        
            Team team = await teamRepository.GetTeamById(teamId);

            var isAuthUserIsLeader = team.LeaderId == userId ? true : false;

            if(!isAuthUserIsLeader)
            {
                throw new UnauthorizedAccessException("this operation is for the leader only");
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
            }

            User user = await userRepository.GetUserByUsername(newLeaderUsername);
            
<<<<<<< HEAD
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

=======
            
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



>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
        public async Task<User> GetUserByUsername(string username)
        {
            return await userRepository.GetUserByUsername(username);

        }

<<<<<<< HEAD
        public async Task<HashSet<Room>> getAuthUserRooms(string userId)
=======
        public async Task<HashSet<RoomDto>> getAuthUserRooms(string userId)
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
        {
            Console.WriteLine("Authenticad User = " + userId);
            List<Team> teams =await userRepository.getUserTeams(userId);

            HashSet<Room> rooms = new HashSet<Room>();

            foreach(var team in teams)
            {
                rooms.Add(await roomRepository.GetRoomById(team.RoomId));
<<<<<<< HEAD
            }    
            return rooms;
=======
            }
            
            return rooms.Select(r => mapper.Map<RoomDto>(r)).ToHashSet();
             
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
        }
    }
}
