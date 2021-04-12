using System;

namespace work_platform_backend.Dtos
{
    public class AttachmentDto
    {
         public int Id { get; set; }

        public string Name { get; set; }
        
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TaskId { get; set; }

    }
}