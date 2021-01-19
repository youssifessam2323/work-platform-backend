using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace work_platform_backend.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public User Creator { get; set; }
        
        public string RoomId { get; set; }
        
        
        public Room Room { get; set; }
        
        public List<Team> SubTeams { get; set; }
     
        public List<RTask> Tasks { get; set; }
        
        
    }
}