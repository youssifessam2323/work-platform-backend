using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using work_platform_backend.Dtos;
using work_platform_backend.Exceptions;
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class ProjectManagerService
    {
        private IRoomRepository roomRepository ;
        private IMapper mapper;
        private  IUserRepository userRepository;

        public ProjectManagerService(IRoomRepository roomRepository, IMapper mapper, IUserRepository userRepository)
        {
            this.roomRepository = roomRepository;
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task AddNewProjectManager(string userId,int roomId)
        {
            ProjectManager projectManager = new ProjectManager();

            projectManager.UserId = userId;
            projectManager.RoomId = roomId;

            await roomRepository.AddNewProjectManager(projectManager);
            await roomRepository.SaveChanges();
        }

        public async Task<List<UserDto>> GetProjectManagersByRoom(int roomId)
        {
            if(!(await isRoomExist(roomId)))
            {
                throw new RoomNotFoundException("room not exist");
            }

            List<User> users = await roomRepository.GetRoomPorjectManagersByRoomId(roomId);

            return users.Select(u => mapper.Map<UserDto>(u)).ToList();
        }

        public async Task<bool> IsProjectManagerExists(string userId,int roomId)
        {
            return await roomRepository.IsProjectManagerExist(userId,roomId);
        }
        
        

        private async Task<bool> isRoomExist(int roomId)
        {
            Room r = await roomRepository.GetRoomById(roomId);

            if(r == null)
            {
                return false;
            }

            return true;
        }


        public async Task AddProjectManagers(int roomId, List<string> usernames )
        {

            usernames.ForEach(u => Console.WriteLine("username ===> " + u ));
            

            List<User> users = new List<User>();

            foreach(var username in usernames )
            {
                var user = await userRepository.GetUserByUsername(username);
                
                if(user == null)
                {
                    continue;
                }
                users.Add(user);
            }

            users.ForEach(u => Console.WriteLine(u));


            Room room = await roomRepository.GetRoomOnlyById(roomId); 

            Console.WriteLine("Room ==> " + room );
            
            foreach(var user in users)
            {

            
                var isProjectManagerExist = await  IsProjectManagerExists(user.Id,room.Id);
                if(isProjectManagerExist)
                {
                    continue;
                }

                // User user = await userRepository.GetUserById(userId);
                await AddNewProjectManager(user.Id,room.Id);

            }
          
        }

        public async Task RemoveFromProjectManagers(int roomId, List<string> usernames)
        {
            List<User> users = new List<User>();

            foreach(var username in usernames )
            {
                var user = await userRepository.GetUserByUsername(username);
                users.Add(user);
            }


            Room room = await roomRepository.GetRoomById(roomId); 

            foreach(var user in users)
            {
                await RemoveProjectManager(user.Id,roomId);
            }
        }

        private async Task RemoveProjectManager(string userId, int roomId)
        {
            await roomRepository.RemoveProjectManagerbyRoomAndUser(roomId,userId);
            await roomRepository.SaveChanges();
        }
    }
}