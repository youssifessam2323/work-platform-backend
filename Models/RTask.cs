using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace work_platform_backend.Models
{
    public class RTask
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }


        public DateTime PlannedStartDate { get; set; }
        public DateTime PlannedEndDate { get; set; }
        
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }

        public bool? IsFinished { get; set; }
        public int Progress { get; set; }
        public int? TeamId { get; set;}
        public Team Team { get; set; }
        public string CreatorId { get; set; }        
        public User Creator { get; set; }
        public List<Attachment> Attachments { get; set; }
        public int? ProjectId { get; set; }
        public Project Project { get; set; }
        [ForeignKey("DependantTaskId")]
        public List<RTask> DependantTasks { get; set; }
        public int? ParentCheckPointId { get; set; }
        public CheckPoint ParentCheckPoint { get; set; }
        public List<UserTask> UserTasks { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Session> Sessions { get; set; }
        public List<CheckPoint> ChildCheckPoints { get; set; }

        public override string ToString()
        {
            return "ID = " + Id + " ,Name = " + Name + "  " + Sessions.Select( c => c.ToString());
        }
    }
}