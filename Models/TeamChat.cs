namespace work_platform_backend.Models
{
    public class TeamChat
    {   
        public int Id { get; set; }
        public string ChatName { get; set; }
        
        public int TeamId { get; set; }
        public Team Team { get; set; }
        
        public string CreatorId { get; set; }
        
        public User Creator { get; set; }
        
        //add message entity
        
        
    
    }
}