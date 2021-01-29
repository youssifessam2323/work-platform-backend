using AutoMapper;
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
        private readonly IRoomRepository _roomRepo;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
 

        public RoomService(IRoomRepository roomRepository,IMapper mapper ,IHttpContextAccessor httpContextAccessor)
        {
            _roomRepo = roomRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            
        }

        private string GetUserId() => (_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
      
     

        public async Task<Room> AddRoom(RequestRoomDto requestRoomDto , string creatorId)
        {
            if (requestRoomDto != null)
            {
                
                var newRoom =  _mapper.Map<Room>(requestRoomDto);
                newRoom.CreatedAt = DateTime.Now ;
                newRoom.CreatorId = creatorId;
                await _roomRepo.SaveRoom(newRoom);
                await _roomRepo.SaveChanges();
                 return newRoom;
            }
            return null;

        }

        public async Task<Room> UpdateRoom(int id, RequestRoomDto room)
        {
            Room UpdatedRoom = await _roomRepo.UpdateRoomById(id, room);

            if (UpdatedRoom != null)
            {
                await _roomRepo.SaveChanges();
                return UpdatedRoom;
            }


            return null;

        }


        public async Task DeleteRoom(int roomId)
        {
            var room = await _roomRepo.DeleteRoomById(roomId);
            if (room == null)
            {

                throw new NullReferenceException();

            }

            await _roomRepo.SaveChanges();


        }



        public async Task<ResponseRoomDto> GetRoom(int roomId)
        {
            Room Room = await _roomRepo.GetRoomById(roomId);

            if (Room != null)
            {

                var newRoomResponse = _mapper.Map<ResponseRoomDto>(Room);
                return newRoomResponse;
            }


            return null;

        }

        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            var Rooms = await _roomRepo.GetAllRooms();

            if (Rooms .Count()!= 0)
            {

                return Rooms;
            }


            return null;

        }

        public async Task<IEnumerable<ResponseRoomDto>> GetAllRoomsOfCreator(string CreatorId)
        {
            
            var Rooms = await _roomRepo.GetRoomsByCreator(CreatorId);

            if (Rooms.Count() != 0)
            {
                var RoomResponse = _mapper.Map<IEnumerable<ResponseRoomDto>>(Rooms);
                
                return RoomResponse;
            }


            return null;

        }

  





    }
}
