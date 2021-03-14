using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace work_platform_backend.Models
{
    public class User : IdentityUser
    {
        
        public string Name { get; set; }
  
        public DateTime BirthDate { get; set; }

        public string ImageUrl { get; set; }
        
        public string JobTitle { get; set; }
        

        public DateTime CreatedAt { get; set; }
        
        public bool IsEnabled { get; set; }
        
        public List<ProjectManager> ManagedProjects { get; set; }
        
        [JsonIgnore]
        public List<Team> Leads { get; set; }
  
        public List<RTask> OwnedTasks { get; set; }
  
        public List<TeamsMembers> TeamMembers { get; set; }
        [JsonIgnore]
  
        public List<Room> Rooms { get; set; }


        public List<UserTask> UserTasks { get; set; }
        
        public List<Project> OwnedProjects { get; set; }
        
        public List<Session> Sessions { get; set; }
        
        public List<Comment> Comments { get; set; }
        
        
        
        

    }
}