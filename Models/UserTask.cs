namespace work_platform_backend.Models
{
    public class UserTask
    {
        public string UserId { get; set; }
        
        public User User { get; set; }
        
        public int TaskId { get; set; }
        
        public RTask Task { get; set; }
        
    }
}