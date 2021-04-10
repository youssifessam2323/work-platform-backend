using System;
using System.Collections.Generic;

namespace work_platform_backend.Dtos
{
    public class ProjectDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime PlannedStartDate { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public DateTime ActualStartDate { get; set; }
        public DateTime ActualEndDate { get; set; }
        public bool IsFinished { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserDto Creator { get; set; }
        public RoomDto Room { get; set; }
        public List<TaskDto> Tasks { get; set; }
        public List<TeamDto> TeamProjects { get; set; }




        
    }
}