namespace work_platform_backend.Models
{
    public class TeamsMembers
    {
        
        public int Id { get; set; }
        
        
        public User user { get; set; }
        
        public Team team { get; set; }
        
        
    }
}