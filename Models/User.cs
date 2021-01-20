using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace work_platform_backend.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public List<UserRoomPermission> UserPermissionsInRoom { get; set; }
        public List<ProjectManager> Projects { get; set; }
        public List<Team> Leads { get; set; }
        public List<RTask> TasksCreatedByMe { get; set; }
        public List<TeamsMembers> TeamMembers { get; set; }
        
    }
}