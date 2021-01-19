using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace work_platform_backend.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public DateTime PlannedStartDate { get; set; }
        public DateTime PlannedEndDate { get; set; }
        
        public DateTime ActualStartDate { get; set; }
        public DateTime ActualEndDate { get; set; }
        
        public bool IsFinished { get; set; }
        
        public List<ProjectManager> Managers { get; set; }
        
        
        public Room Room { get; set; }
        public List<RTask> Tasks { get; set; }
        
        
        
        
        
        
        
    }
}