using System.Collections.Generic;
using System.Threading.Tasks;
using work_platform_backend.Dtos;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public interface IRoomRepository
    {
         Task<Room> GetRoomById(int roomId);
         Task<bool> isRoomExist(int roomId);
        Task<IEnumerable<Room>> GetRoomsByCreator(string creatorId);
        Task<IEnumerable<Room>> GetAllRooms();
         Task SaveRoom(Room room );
         Task<Room> UpdateRoomById(int roomId, Room room);
         Task<Room> DeleteRoomById(int roomId);
        Task<bool> RemoveProjectManagerbyRoom(int roomId);
         Task<bool> SaveChanges();
        Task AddNewProjectManager(ProjectManager projectManager);
        Task<List<User>> GetRoomPorjectManagersByRoomId(int roomId);
        Task<Room> GetRoomByName(string name);
        Task<bool> IsProjectManagerExist(string userId,int roomId);
        Task<bool> IsRoomExist(int roomId);
        Task RemoveProjectManagerbyRoomAndUser(int roomId, string userId);
        Task<Room> GetRoomOnlyById(int roomId);
    }
}