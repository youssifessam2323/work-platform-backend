using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace work_platform_backend.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }

        public string FromUserId { get; set; }
        public User FromUser { get; set; }
        public int ToTeamChatId { get; set; }
        public TeamChat ToTeamChat{ get; set; }
         public int MessageTypeId { get; set; } 
        public ChatMessageType  MessageType { get; set; }
    }
}
