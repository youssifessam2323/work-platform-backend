using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public class RoomRepo : IRoomRepository
    {
        private readonly ApplicationContext _context;

        public RoomRepo(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Room> GetRoomById(int roomId)
        {
          return ( await _context.Rooms.FirstOrDefaultAsync(R=>R.Id==roomId));
        }

        public async Task SaveRoom(Room room)
        {
           await _context.Rooms.AddAsync(room);
        }

        public async Task<Room> UpdateRoomById(int roomId, Room room)
        {
            var NewRoom = await _context.Rooms.FindAsync(roomId);
            if (NewRoom != null)
            {
                NewRoom.Name = room.Name;
                NewRoom.Description = room.Description;
                NewRoom.CreatedAt = room.CreatedAt;
                return NewRoom;
            }
            return null;
        }

        public async Task<Room> DeleteRoomById(int roomId)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room != null)
            {
                _context.Rooms.Remove(room);
            }
            return room;
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
