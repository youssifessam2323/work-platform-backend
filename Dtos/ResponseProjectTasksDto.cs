using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Dtos
{
    public class ResponseProjectTasksDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Team Team { get; set; }
    }
}
