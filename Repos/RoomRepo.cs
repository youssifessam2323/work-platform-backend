using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Dtos;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public class RoomRepo : IRoomRepository
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public RoomRepo(ApplicationContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Room> GetRoomById(int roomId)
        {
          return ( await _context.Rooms.FirstOrDefaultAsync(R=>R.Id==roomId));
        }

        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            return (await _context.Rooms.ToListAsync());
        }

        public async Task<IEnumerable<Room>> GetRoomsByCreator(string creatorId)
        {
            return (await _context.Rooms.Include(R => R.Creator).Where(R=>R.CreatorId==creatorId).ToListAsync());
        }

        public async Task SaveRoom( Room room)
        {
           await _context.Rooms.AddAsync(room);
        }

        public async Task<Room> UpdateRoomById(int roomId, RequestRoomDto requestRoomDto)
        {
            var NewRoom = await _context.Rooms.FindAsync(roomId);
            if (NewRoom != null)
            {
                var newRoom = _mapper.Map<Room>(requestRoomDto);
                newRoom.CreatedAt = DateTime.Now;

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
