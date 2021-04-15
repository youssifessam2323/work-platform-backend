using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using work_platform_backend.Dtos;
using work_platform_backend.Exceptions;
using work_platform_backend.Exceptions.Room;
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

        private readonly IProjectRepository projectRepository;
        public readonly ITeamRepository teamRepo;
        private readonly SettingService settingService;
        private readonly ProjectService projectService;

        public RoomService(IRoomRepository roomRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, TeamService teamService = null, IProjectRepository projectRepository = null, ITeamRepository teamRepo=null,SettingService settingService=null,ProjectService projectService=null)
        {
            this.roomRepository = roomRepository;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.teamService = teamService;
            this.projectRepository = projectRepository;
            this.teamRepo = teamRepo;
            this.settingService = settingService;
            this.projectService = projectService;
        }

        private string GetUserId() => (httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
      
     

        public async Task AddRoom(RoomDto roomDto , string creatorId)
        {
                var room = mapper.Map<Room>(roomDto);

                Room isRoomNameNotUnique = await roomRepository.GetRoomByName(room.Name);

                if(isRoomNameNotUnique != null)
                {
                    throw new RoomNameMustBeUniqueException();
                }
                
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
            
            Room updatedRoom = await roomRepository.UpdateRoomById(id, room);

            if (updatedRoom != null)
            {
                await roomRepository.SaveChanges();
                Console.WriteLine("Updated Room = " + updatedRoom);
                return updatedRoom;
            }
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
        }


        public async Task DeleteRoom(int roomId)
        {
            if(await isRoomExist(roomId))
            {

                //Must Delete Projectmanager

            
                await roomRepository.DeleteRoomById(roomId);

                await teamService.DeleteTeamByRoom(roomId);

                await projectService.DeleteProjectByRoom(roomId);  //Delete on Project not working
                var settingsofRoom =  await settingService.GetSettingsOfRoom(roomId);

                if(settingsofRoom!=null)
                {
                    foreach(Setting setting in settingsofRoom)
                    {
                        await settingService.RemoveSettingfromRoom(roomId, setting.Id);
                    }
                }
                
                
                
                await roomRepository.SaveChanges();

            }
            throw new RoomNotFoundException("room not exist");

        }


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
