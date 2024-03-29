﻿using AutoMapper;
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
          var room =  await context.Rooms
                            .Include(r => r.Creator)
                            .Include(r => r.Projects)
                            .Include(r => r.ProjectManagers).ThenInclude(p => p.User)
                            .Include(r => r.Teams).ThenInclude(t => t.Leader)
                            .FirstOrDefaultAsync(R=>R.Id==roomId);

            return room;
        }

        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            return (await context.Rooms
                                .Include(r => r.Creator)
                                .Include(r => r.Projects)
                                .Include(r => r.Teams)
                                .Include(r => r.ProjectManagers)
                                .ToListAsync());
        }

        public async Task<IEnumerable<Room>> GetRoomsByCreator(string creatorId)
        {
            return (await context.Rooms.Include(R => R.Creator).Where(R=>R.CreatorId==creatorId).ToListAsync());
        }

        public async Task SaveRoom( Room room)
        {
           await context.Rooms.AddAsync(room);
        }

        public async Task<Room> UpdateRoomById(int roomId, Room room)
        {
            var newRoom = await context.Rooms.FindAsync(roomId);
            {
                if (newRoom != null)
                {
             
                    if (!string.IsNullOrEmpty( room.Name))
                    {
                        newRoom.Name = room.Name;
                       
                    }
                    if (!string.IsNullOrEmpty(room.Description))
                    {
                        newRoom.Description = room.Description;
                    }
                    newRoom.CreatedAt = DateTime.Now;
                    return newRoom;
                }
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

        

        public async Task<bool> RemoveProjectManagerbyRoom(int roomId)
        {
            var projectManagers = await context.ProjectManagers
                                                    .Where(r => r.RoomId == roomId)
                                                    .ToListAsync();

            if (projectManagers.Count().Equals(0))
            {
                return false;
            }
            foreach (ProjectManager pm in projectManagers)
            {
                context.ProjectManagers.Remove(pm);
            }

            return true;
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

        public async Task<Room> GetRoomByName(string name)
        {
            return await context.Rooms.Where(r => r.Name == name).SingleOrDefaultAsync();
        }

        public async Task<bool> IsProjectManagerExist(string userId, int roomId)
        {
            ProjectManager projectManager = await context.ProjectManagers
                    .Where(pm => pm.UserId == userId && pm.RoomId == roomId)
                    .SingleOrDefaultAsync();

            if(projectManager != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> isRoomExist(int roomId)
        {
            var room = await context.Rooms.Where(r => r.Id == roomId).SingleOrDefaultAsync();
            
            return  room != null ?  true : false;  
        }

        public async Task<bool> IsRoomExist(int roomId)
        {
            var room = await context.Rooms.FindAsync(roomId);
            return room != null ? true : false ; 
        }

        public async Task RemoveProjectManagerbyRoomAndUser(int roomId, string userId)
        {
            ProjectManager manager = await context.ProjectManagers
                                                        .Where(pm => pm.UserId == userId && pm.RoomId == roomId)
                                                        .SingleOrDefaultAsync();


            context.ProjectManagers.Remove(manager);
        }

        public async Task<Room> GetRoomOnlyById(int roomId)
        {
            return await context.Rooms.FindAsync(roomId);
        }
    }
}
