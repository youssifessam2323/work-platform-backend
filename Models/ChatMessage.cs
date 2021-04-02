using System;

namespace work_platform_backend.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }

        public string CreatorId { get; set; }
        public User Creator { get; set; }
        public int ChatId { get; set; }
        public TeamChat Chat{ get; set; }
         public int? MessageTypeId { get; set; } 
        public ChatMessageType  MessageType { get; set; }
    }
}
