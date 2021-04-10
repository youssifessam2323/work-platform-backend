using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public interface IChatMessageRepository
    {
        Task SaveMessage(ChatMessage chatMessage);
        Task<ChatMessage> DeleteMessageById(int chatMessageId);
        Task<ChatMessage> GetMessageById(int chatMessageId);
        Task<IEnumerable<ChatMessage>> GetMessageHistorybyChat(int chatId);
        Task <ICollection<ChatMessage>> GetAllMessageByUser(string userId);       
        Task<ICollection<ChatMessage>> GetAllMessageByTeamCHat(int TeamChatId);
        Task<ICollection<ChatMessage>> GetAllMessageByMessageType(int chatmessageTypeId);
        Task<bool> SaveChanges();
    }
}
