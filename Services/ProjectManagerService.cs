using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Threading.Tasks;
=======
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using work_platform_backend.Dtos;
using work_platform_backend.Exceptions;
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class ProjectManagerService
    {
        private IRoomRepository roomRepository ;
<<<<<<< HEAD

        public ProjectManagerService(IRoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
        }

        public async Task AddNewProjectManager(User user,Room room)
        {
            ProjectManager projectManager = new ProjectManager();

            projectManager.User = user;
            projectManager.Room = room;
=======
        private IMapper mapper;

        public ProjectManagerService(IRoomRepository roomRepository,IMapper mapper)
        {
            this.roomRepository = roomRepository;
            this.mapper = mapper;
        }

        public async Task AddNewProjectManager(string userId,int roomId)
        {
            ProjectManager projectManager = new ProjectManager();

            projectManager.UserId = userId;
            projectManager.RoomId = roomId;
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca

            await roomRepository.AddNewProjectManager(projectManager);
            await roomRepository.SaveChanges();
        }

<<<<<<< HEAD
        public async Task<List<User>> GetProjectManagersByRoom(int roomId)
        {
            return await roomRepository.GetRoomPorjectManagersByRoomId(roomId);
=======
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
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
        }
    }
}