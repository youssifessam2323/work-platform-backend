using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Dtos
{
    public class ResponseTeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ResponseTeamRTaskDto> Tasks { get; set; }
        
    }
}
