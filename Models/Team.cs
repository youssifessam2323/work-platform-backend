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

        // under testing 
        public Guid TeamCode { get; set; }
        
                
        public string LeaderId { get; set; }
            
        
        public User Leader { get; set; }
        
        public int RoomId { get; set; }

        public Room Room { get; set; }
        
        public List<Team> SubTeams { get; set; }

        [JsonIgnore]
        public List<RTask> Tasks { get; set; }

        public List<TeamsMembers> TeamMembers { get; set; }

        public List<TeamProject> TeamProjects { get; set; }

        public TeamChat TeamChat { get; set; }
        
        
        
        public override string ToString()
        {
            return "Id = " + Id + " Name = " + Name ; 
        }
        
        
        
        
    }
}