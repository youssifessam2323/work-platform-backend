namespace work_platform_backend.Models
{
    public class TeamProject
    {
        public int ProjectId { get; set; }
        
        public Project Project { get; set; }
        
        public int TeamId { get; set; }
        
        public Team Team { get; set; }
        
        
    }
}