namespace work_platform_backend.Models
{
    public class TeamsMembers
    {
        
        
        public string UserId { get; set; }
        public User User { get; set; }
        
        public int TeamId { get; set; }
        
        public Team Team { get; set; }


        public override string ToString()
        {
            return "UserId = " + UserId + "Team = " + Team; 
        }

        
    }
}