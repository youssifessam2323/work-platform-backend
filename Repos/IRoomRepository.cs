using System.Collections.Generic;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public interface IRoomRepository
    {
         Task<Room> GetRoomById(int roomId);
         Task SaveRoom(Room room );
         Task UpdateRoomById(int roomId, Room room);
         Task DeleteRoomById(int roomId);
    }
}