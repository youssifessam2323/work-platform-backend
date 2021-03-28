using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Dtos
{
    public class ResponseRoomDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }
  
        public ResponseRoomCreatorDto Creator { get; set; }

    
    }
}
