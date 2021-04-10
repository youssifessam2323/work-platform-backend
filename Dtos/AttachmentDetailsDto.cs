using System;

namespace work_platform_backend.Dtos
{
    public class AttachmentDetailsDto
    {
           public int Id { get; set; }

        public string Name { get; set; }
        
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public TaskDto Task { get; set; }
        
        public UserDto Creator { get; set; }
        
    }
}