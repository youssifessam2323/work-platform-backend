using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace work_platform_backend.Models
{
    public class CheckPoint
    {
        public int Id { get; set; }
        public string Text { get; set; }
        
        public bool IsChecked { get; set; }
        
        public int ParentRTaskId { get; set; }        
        public RTask ParentRTask { get; set; }
        public List<RTask> SubTasks { get; set; }
        
        
        
        

    }
}