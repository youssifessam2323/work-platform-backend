using System;

namespace work_platform_backend.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string text { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}