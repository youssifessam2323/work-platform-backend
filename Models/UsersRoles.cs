namespace work_platform_backend.Models
{
    public class UsersRoles
    {
        public int UserId { get; set; }
        public User user { get; set; }
        
        public int RoleId { get; set; }
        
        public Role role { get; set; }
        
        
        
    }
}