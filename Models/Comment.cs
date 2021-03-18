using System;
using System.Collections.Generic;

namespace work_platform_backend.Models
{
    public class Comment
    {
        public int Id { get; set; }
        
        public string text { get; set; }
        
        public DateTime CreatedAt { get; set; }

        
        public int TaskId { get; set; }        
        public RTask Task { get; set; }
        
        public string CreatorId { get; set; }
        
        public User Creator { get; set; }
                
    
        public List<Comment> Replies{set;get;}
        
        
    }
}