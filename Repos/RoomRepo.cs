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
        private readonly ApplicationContext context;
        private readonly IMapper _mapper;

        public RoomRepo(ApplicationContext context,IMapper mapper)
        {
            this.context = context;
            _mapper = mapper;
        }

        public async Task<Room> GetRoomById(int roomId)
        {
          return ( await context.Rooms.Include(r => r.Creator).FirstOrDefaultAsync(R=>R.Id==roomId));
        }

        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            return (await context.Rooms.Include(r => r.Creator).ToListAsync());
        }

        public async Task<IEnumerable<Room>> GetRoomsByCreator(string creatorId)
        {
            return (await context.Rooms.Include(R => R.Creator).Where(R=>R.CreatorId==creatorId).ToListAsync());
        }

        public async Task SaveRoom( Room room)
        {
           await context.Rooms.AddAsync(room);
        }

        public async Task<Room> UpdateRoomById(int roomId, RoomRequest roomRequest)
        {
            var newRoom = await context.Rooms.FindAsync(roomId);
            if (newRoom != null)
            {
                // var newRoom = _mapper.Map<Room>(roomRequest);
                newRoom.Name = roomRequest.Name;
                newRoom.Description= roomRequest.Description;
                newRoom.CreatedAt = DateTime.Now;
                
                return newRoom;
            }
            return null;
        }

        public async Task<Room> DeleteRoomById(int roomId)
        {
            var room = await context.Rooms.FindAsync(roomId);
            if (room != null)
            {
                context.Rooms.Remove(room);
            }
            return room;
        }

        public async Task AddNewProjectManager(ProjectManager projectManager)
        {
            await context.ProjectManagers.AddAsync(projectManager);
        }

        public async Task<bool> SaveChanges()
        {
            return (await context.SaveChangesAsync() >= 0);
        }

        public async Task<List<User>> GetRoomPorjectManagersByRoomId(int roomId)
        {
            IQueryable<ProjectManager> projectManagers = context.ProjectManagers
                    .Include(pm => pm.User).Where(pm => pm.RoomId == roomId);

             return await projectManagers.Select(pm => pm.User).ToListAsync();
        }
    }
}
