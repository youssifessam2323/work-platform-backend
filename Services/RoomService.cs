﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using work_platform_backend.Dtos;
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class RoomService
    {
        private readonly IRoomRepository roomRepository;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly TeamService teamService;
        private  IProjectRepository projectRepository { get; set; }



        public RoomService(IRoomRepository roomRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, TeamService teamService = null, IProjectRepository projectRepository = null)
        {
            this.roomRepository = roomRepository;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.teamService = teamService;
            this.projectRepository = projectRepository;
        }

        private string GetUserId() => (httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
      
     

        public async Task<Room> AddRoom(Room room , string creatorId)
        {
                
                room.CreatedAt = DateTime.Now ;
                room.CreatorId = creatorId;
                await roomRepository.SaveRoom(room);
                await roomRepository.SaveChanges();

                if (room != null)
                {
                    
            
                    Team newTeam = new Team()
                    {
                        Name = $" {room.Name}/main ",
                        Description = room.Description,
                        CreatedAt = DateTime.Now,
                    };

                await teamService.AddTeam(newTeam,room.Id,creatorId);

                return room;
                }
                return null;
            }

        public async Task<List<Project>> GetRoomProjects(int roomId)
        {
            return (List<Project>)(await projectRepository.GetAllProjectsByRoom(roomId));

        }

        public async Task<Room> UpdateRoom(int id, Room room)
        {
            Room updatedRoom = await roomRepository.UpdateRoomById(id, room);

            if (updatedRoom != null)
            {
                await roomRepository.SaveChanges();
                Console.WriteLine("Updated Room = " + updatedRoom);
                return updatedRoom;
            }


            return null;

        }


        public async Task DeleteRoom(int roomId)
        {
            var room = await roomRepository.DeleteRoomById(roomId);
            if (room == null)
            {

                throw new NullReferenceException();

            }

            await roomRepository.SaveChanges();


        }



        public async Task<Room> GetRoom(int roomId)
        {
            Room room = await roomRepository.GetRoomById(roomId);

            if (room != null)
            {
                return room;
            }


            return null;

        }

        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            var Rooms = await roomRepository.GetAllRooms();

            if (Rooms .Count()!= 0)
            {

                return Rooms;
            }


            return null;

        }

        public async Task<IEnumerable<Room>> GetAllRoomsOfCreator(string CreatorId)
        {
            
            var rooms = await roomRepository.GetRoomsByCreator(CreatorId);

            if (rooms.Count() != 0)
            {
                return rooms;
            }


            return null;

        }

  





    }
}
