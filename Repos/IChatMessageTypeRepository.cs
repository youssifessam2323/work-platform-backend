using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
   public interface IChatMessageTypeRepository
    {
        Task SaveMessageType(ChatMessageType chatMessageType);
        Task<ChatMessageType> DeleteMessageTypeById(int chatMessageTypeId);
        Task<ChatMessageType> GetMessageTypeById(int chatMessageTypeId);
        Task <ChatMessageType>UpdateMessageTypeById(int chatMessageTypeId,ChatMessageType chatMessageType);

        Task<bool> SaveChanges();



    }
}
