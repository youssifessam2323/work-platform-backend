using System;
using System.Collections.Generic;

namespace work_platform_backend.Dtos
{
    public class CommentDetailsDto
    {
        public int Id { get; set; }
        public string text { get; set; }        
        public DateTime CreatedAt { get; set; }
        public TaskDto Task { get; set; }
        public UserDto Creator { get; set; }
        public List<CommentDto> Replies{set;get;}


    }
}