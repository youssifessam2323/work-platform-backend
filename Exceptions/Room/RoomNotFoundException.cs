using System;

namespace work_platform_backend.Exceptions
{
    public class RoomNotFoundException : Exception
    {
        public RoomNotFoundException(string message):base(message){}
    }
}