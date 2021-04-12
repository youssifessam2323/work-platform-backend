using System;

namespace work_platform_backend.Exceptions.Room
{
    public class RoomNameMustBeUniqueException : Exception
    {
        public RoomNameMustBeUniqueException():base("room name was taken"){}
    }
}