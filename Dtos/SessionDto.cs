using System;

namespace work_platform_backend.Dtos
{
    public class SessionDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ExtraTime { get; set; }
        public int TaskProgress { get; set; }
        public TaskDto Task { get; set; }
        public UserDto User { get; set; }

        
    }
}