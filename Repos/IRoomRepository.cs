using System.Collections.Generic;
using System.Threading.Tasks;
using work_platform_backend.Dtos;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public interface IRoomRepository
    {
         Task<Room> GetRoomById(int roomId);
        Task<IEnumerable<Room>> GetRoomsByCreator(string creatorId);
        Task<IEnumerable<Room>> GetAllRooms();
         Task SaveRoom(Room room );
         Task<Room> UpdateRoomById(int roomId, RequestRoomDto room);
         Task<Room> DeleteRoomById(int roomId);
         Task<bool> SaveChanges();
    }
}