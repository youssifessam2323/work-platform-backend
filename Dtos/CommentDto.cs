using System;
using System.Collections.Generic;

namespace work_platform_backend.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string text { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TaskId { get; set; }
        public string CreatorId { get; set; }
        public int ParentCommentId { get; set; }
        public List<CommentDto> Replies { get; set; }
        
        
        
    }
}