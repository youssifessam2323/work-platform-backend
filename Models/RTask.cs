using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace work_platform_backend.Models
{
    public class RTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public DateTime PlannedStartDate { get; set; }
        public DateTime PlannedEndDate { get; set; }
        
        public DateTime ActualStartDate { get; set; }
        public DateTime ActualEndDate { get; set; }
        
        public bool IsFinished { get; set; }

        public Team Team { get; set; }
        
        public User Creator { get; set; }
        
        public List<CheckPoint> ChildCheckPoints { get; set; }
        
        public List<Attachment> Attachments { get; set; }
        
        public Project Project { get; set; }
        
        public List<DependOn> DependantOnMe { get; set; }
        
        public List<DependOn> DependOnThem { get; set; }

        public int? ParentCheckPointId { get; set; }
        
        
        public CheckPoint ParentCheckPoint { get; set; }
        
               
        
        
    }
}