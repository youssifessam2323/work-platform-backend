using System;
using System.Collections.Generic;

namespace work_platform_backend.Dtos
{
    public class TeamDto
    {
         public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid TeamCode { get; set; }
        public List<TeamDto> SubTeams { get; set; }
        public UserDto Leader { get; set; }

        
        
    }
}