using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class RoomService
    {
        private readonly IRoomRepository _roomRepo;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepo = roomRepository;
        }
        public async Task<Room> AddRoom(Room newRoom)
        {
            if (newRoom != null)
            {
                await _roomRepo.SaveRoom(newRoom);
                await _roomRepo.SaveChanges();
                return newRoom;
            }
            return null;

        }

        public async Task<Room> UpdateRoom(int id, Room room)
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



        public async Task<Room> GetRoom(int roomId)
        {
            Room Room = await _roomRepo.GetRoomById(roomId);

            if (Room != null)
            {

                return Room;
            }


            return null;

        }
    }
}
