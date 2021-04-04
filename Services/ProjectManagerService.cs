using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class ProjectManagerService
    {
        private IRoomRepository roomRepository ;

        public ProjectManagerService(IRoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
        }

        public async Task AddNewProjectManager(User user,Room room)
        {
            ProjectManager projectManager = new ProjectManager();

            projectManager.User = user;
            projectManager.Room = room;

            await roomRepository.AddNewProjectManager(projectManager);
            await roomRepository.SaveChanges();
        }

        public async Task<List<User>> GetProjectManagersByRoom(int roomId)
        {
            return await roomRepository.GetRoomPorjectManagersByRoomId(roomId);
        }
    }
}