using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public class ChatMessageRepository : IChatMessageRepository
    {

        private readonly ApplicationContext context;

        public ChatMessageRepository(ApplicationContext context)
        {
            this.context = context;
        }


        public async Task SaveMessage(ChatMessage chatMessage)
        {
            await context.ChatMessages.AddAsync(chatMessage);
        }

        public async Task<ChatMessage> DeleteMessageById(int chatMessageId)
        {
            ChatMessage chatMessage = await context.ChatMessages.FindAsync(chatMessageId);
            if (chatMessage != null)
            {
                context.ChatMessages.Remove(chatMessage);
            }
            return chatMessage;
        }

        public async Task<ICollection<ChatMessage>> GetAllMessageByMessageType(int chatmessageTypeId)
        {
            return ( await context.ChatMessages.Where(m => m.MessageTypeId == chatmessageTypeId).ToListAsync());
          
        }

        public async Task<ICollection<ChatMessage>> GetAllMessageByTeamCHat(int TeamChatId)
        {
           return( await context.ChatMessages.Where(m => m.ToTeamChatId == TeamChatId).ToListAsync());
           
        }

        public async Task<ICollection<ChatMessage>> GetAllMessageByUser(string userId)
        {
            return( await context.ChatMessages.Where(m => m.FromUserId == userId).ToListAsync());
            
        }

        public async Task<ChatMessage> GetMessageById(int chatMessageId)
        {
            return (await context.ChatMessages.FirstOrDefaultAsync(m => m.Id == chatMessageId));
        }

        public async Task<bool> SaveChanges()
        {
            return (await context.SaveChangesAsync() >= 0);
        }

       
    }

}
