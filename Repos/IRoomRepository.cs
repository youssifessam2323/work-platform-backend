using System.Collections.Generic;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public interface IRoomRepository
    {
         Task<Room> GetRoomById(int roomId);
         Task SaveRoom(Room room );
         Task<Room> UpdateRoomById(int roomId, Room room);
         Task<Room> DeleteRoomById(int roomId);
         Task<bool> SaveChanges();
    }
}