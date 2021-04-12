using System;
using System.Collections.Generic;

namespace work_platform_backend.Dtos
{
    public class TaskDto
    {
         public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime PlannedStartDate { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public DateTime ActualStartDate { get; set; }
        public DateTime ActualEndDate { get; set; }
        public bool IsFinished { get; set; }
        public List<CheckPointDto> ChildCheckPoints { get; internal set; }

    }
}