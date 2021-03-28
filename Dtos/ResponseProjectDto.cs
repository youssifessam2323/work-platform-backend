using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Dtos
{
    public class ResponseProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime PlannedStartDate { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public List<ResponseProjectManagersDto> Managers { get; set; }
        public List<ResponseProjectTasksDto> Tasks { get; set; }
     
    }
}
