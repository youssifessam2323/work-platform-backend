using System;
using System.Collections.Generic;

namespace work_platform_backend.Models
{
    public class Session
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        
        public int? ExtraTime { get; set; }
        public int TaskProgress { get; set; }
        
        public int TaskId { get; set; }
        
        
        public RTask Task { get; set; }
        
        public string UserId { get; set; }
        
        
        public User User { get; set; }
        
        
        
    
    }
}