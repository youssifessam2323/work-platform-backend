using System;
using System.Collections.Generic;

namespace work_platform_backend.Dtos
{
    public class TeamDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public Guid TeamCode { get; set; }
        public UserDto Leader { get; set; }
        public RoomDto Room { get; set; }
        public List<TeamDto> SubTeams { get; set; }
        public List<TaskDto> Tasks { get; set; }
        public List<UserDto> TeamMembers { get; set; }
        public List<ProjectDto> TeamProjects { get; set; }
        public TeamChatDto TeamChat { get; set; }




    }
}