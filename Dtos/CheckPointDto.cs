using System.Collections.Generic;

namespace work_platform_backend.Dtos
{
    public class CheckPointDto
    {
         public int Id { get; set; }
        public string CheckpointText { get; set; }
        public string Description { get; set; }
        public int Percentage { get; set; }
         public List<TaskDto> SubTasks { get; set; }

    }
}