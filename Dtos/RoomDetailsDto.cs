using System;
using System.Collections.Generic;

namespace work_platform_backend.Dtos
{
    public class RoomDetailsDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserDto Creator { get; set; }
        public List<ProjectDto> Projects { get; set; }
        public List<TeamDto> Teams { get; set; }
        public List<UserDto> ProjectManagers { get; set; }
        
        

    }
}