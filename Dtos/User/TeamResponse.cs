using System;
using work_platform_backend.Models;

namespace work_platform_backend.Dtos.Response
{
    public class TeamResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public string TeamCode { get; set; }
        
        
        
        
        
    }
}