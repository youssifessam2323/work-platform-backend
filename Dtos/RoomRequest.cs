using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace work_platform_backend.Dtos
{
    public class RoomRequest
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

       

       
    }
}
