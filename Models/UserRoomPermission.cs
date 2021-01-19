using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace work_platform_backend.Models
{
    public class UserRoomPermission
    {
        


        public string UserId { get; set; }

        public User User { get; set; }
        

        public int RoomId { get; set; }

        public Room Room { get; set; }
        
        public Permission Permission { get; set; }
        
        
    }
}