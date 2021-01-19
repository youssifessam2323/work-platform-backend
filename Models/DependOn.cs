using System;

namespace work_platform_backend.Models
{
    public class DependOn
    {   
        public int Id { get; set; }
        public int RTaskId { get; set; }
        public RTask RTask { get; set; }

        
        public int DependantTaskId { get; set; }
        public RTask DependantTask { get; set; }
        
        
        
    }
}