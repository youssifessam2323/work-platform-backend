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
    }
}