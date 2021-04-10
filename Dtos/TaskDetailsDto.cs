using System;
using System.Collections.Generic;
using work_platform_backend.Models;

namespace work_platform_backend.Dtos
{
    public class TaskDetailsDto
    {
         public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime PlannedStartDate { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public DateTime ActualStartDate { get; set; }
        public DateTime ActualEndDate { get; set; }
        public bool IsFinished { get; set; }
        public TeamDto Team { get; set; }
        public UserDto Creator { get; set; }
        public List<AttachmentDto> Attachments { get; set; }
        public ProjectDto Project { get; set; }
        public List<TaskDto> DependantTasks { get; set; }
        public CheckPointDto ParentCheckPoint { get; set; }
        public List<UserDto> AssignedUsers { get; set; }
        public List<CommentDto> Comments { get; set; }
        public List<Session> Sessions { get; set; }
        public List<CheckPointDto> ChildCheckPoints { get; internal set; }


        



    }
}