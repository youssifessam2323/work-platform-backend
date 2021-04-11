using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using work_platform_backend.Dtos;
<<<<<<< HEAD
=======
using work_platform_backend.Exceptions;
using work_platform_backend.Exceptions.Room;
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
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
      
     

<<<<<<< HEAD
        public async Task<Room> AddRoom(Room room , string creatorId)
        {
=======
        public async Task AddRoom(RoomDto roomDto , string creatorId)
        {
                var room = mapper.Map<Room>(roomDto);

                Room isRoomNameNotUnique = await roomRepository.GetRoomByName(room.Name);

                if(isRoomNameNotUnique != null)
                {
                    throw new RoomNameMustBeUniqueException();
                }
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
                
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

<<<<<<< HEAD
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
=======
                }
                throw new  RoomNotFoundException("Error While Saving the Room");
            }





        public async Task<List<Project>> GetRoomProjects(int roomId)
        {
            if(!(await isRoomExist(roomId)))
            {
                throw new RoomNotFoundException("room not exist");
            }
            
            Room room = await roomRepository.GetRoomById(roomId);
            if(room == null)
            {
                throw new RoomNotFoundException("Room not exist");
            }
            return (List<Project>)await projectRepository.GetAllProjectsByRoom(roomId);
        }

        public async Task<Room> UpdateRoom(int id, RoomDto roomDto)
        {
            var room = mapper.Map<Room>(roomDto);

            if(!(await isRoomExist(id)))
            {
                throw new RoomNotFoundException("room not exist");
            }


            if(!(await isRoomNameUnique(roomDto.Name)))
            {
                throw new RoomNameMustBeUniqueException();
            }
            
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
            Room updatedRoom = await roomRepository.UpdateRoomById(id, room);

            if (updatedRoom != null)
            {
                await roomRepository.SaveChanges();
                Console.WriteLine("Updated Room = " + updatedRoom);
                return updatedRoom;
            }
<<<<<<< HEAD


            return null;

=======
            throw new RoomNotFoundException("Room not exist");

        }

        private async Task<bool> isRoomNameUnique(string name)
        {
            var room = await roomRepository.GetRoomByName(name);
            if(room != null)
            {
                return false;
            }
            return true;
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
        }


        public async Task DeleteRoom(int roomId)
        {
<<<<<<< HEAD
            var room = await roomRepository.DeleteRoomById(roomId);
            if (room == null)
            {

                throw new NullReferenceException();

            }

            await roomRepository.SaveChanges();

=======
            if(await isRoomExist(roomId))
            {
                await roomRepository.DeleteRoomById(roomId);
                await roomRepository.SaveChanges();
            }
            throw new RoomNotFoundException("room not exist");
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca

        }


<<<<<<< HEAD

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

=======
        private async Task<bool> isRoomExist(int roomId)
        {
            Room r = await roomRepository.GetRoomById(roomId);

            if(r == null )
            {
                return false;
            }

            return true ;
        }


        public async Task<RoomDetailsDto> GetRoom(int roomId)
        {
            Room room = await roomRepository.GetRoomById(roomId);
            RoomDetailsDto roomDetailsDto = mapper.Map<RoomDetailsDto>(room);
            if (room != null)
            {
                return roomDetailsDto;
            }
            throw new RoomNotFoundException("Room not exist");
        }

        public async Task<IEnumerable<RoomDetailsDto>> GetAllRooms()
        {
            List<Room> rooms = (List<Room>)await roomRepository.GetAllRooms();
            List<RoomDetailsDto> roomResponses = rooms.Select(r => mapper.Map<RoomDetailsDto>(r)).ToList(); 
            return roomResponses;
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
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
