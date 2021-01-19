using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace work_platform_backend.Models
{
    public class Room
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public List<Project> Projects { get; set; }
        public List<Setting> Settings { get; set; }
        public List<UserRoomPermission> UserPemissionInRoom { get; set; }
        
        
        
                
        
        
        
        
        
    }
}