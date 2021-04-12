using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public class ChatMessageTypeRepository : IChatMessageTypeRepository
    {

        private readonly ApplicationContext context;

        public ChatMessageTypeRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task SaveMessageType(ChatMessageType chatMessageType)
        {
            await context.ChatMessageTypes.AddAsync(chatMessageType);
        }


        public async Task<ChatMessageType> UpdateMessageTypeById(int chatMessageTypeId, ChatMessageType chatMessageType)
        {
            var NewUpdatedChatMessageType = await context.ChatMessageTypes.FindAsync(chatMessageTypeId);
            if (NewUpdatedChatMessageType != null)
            {
                NewUpdatedChatMessageType.MessageTypeName = chatMessageType.MessageTypeName;
                
                return NewUpdatedChatMessageType;
            }
            return null;
        }

        public async Task<ChatMessageType>  DeleteMessageTypeById(int chatMessageTypeId)
        {
            ChatMessageType chatMessageType = await context.ChatMessageTypes.FindAsync(chatMessageTypeId);
            if (chatMessageType != null)
            {
                context.ChatMessageTypes.Remove(chatMessageType);
            }
            return chatMessageType;
        }

        public async Task<ChatMessageType> GetMessageTypeById(int chatMessageTypeId)
        {
            return (await context.ChatMessageTypes.FirstOrDefaultAsync(mt => mt.Id == chatMessageTypeId));
        }

       

     

     

        public async Task<bool> SaveChanges()
        {
            return (await context.SaveChangesAsync() >= 0);
        }

       

       
    }
}
