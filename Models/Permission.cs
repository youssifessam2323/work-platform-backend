using System.Collections.Generic;

namespace work_platform_backend.Models
{
    public class Permission
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public List<UserRoomPermission> UserPermissionInRoom { get; set; }
        
        
    }
}