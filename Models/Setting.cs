using System.Collections.Generic;

namespace work_platform_backend.Models
{
    public class Setting
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public bool value { get; set; }
        public List<RoomSettings> RoomSettings { get; set; }
        
        
        
        
    }
}