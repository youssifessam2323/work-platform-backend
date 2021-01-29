using System;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace work_platform_backend.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public DateTime CreatedAt { get; set; }
        

        public string CreatorId { get; set; }
        
        
        public User Creator { get; set; }
        
        public int RoomId { get; set; }

        public Room Room { get; set; }
        
        public List<Team> SubTeams { get; set; }

        [JsonIgnore]
        public List<RTask> Tasks { get; set; }
        public List<TeamsMembers> TeamMembers { get; set; }
        
        
        
        
    }
}