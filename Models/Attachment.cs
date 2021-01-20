using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace work_platform_backend.Models
{
    public class Attachment
    {   
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }

        public int TaskId { get; set; }
        
            
        public RTask Task { get; set; }
        
        
        
        
        
    }
}