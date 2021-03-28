using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class ChatMessageTypeService
    {
        private readonly IMapper mapper;
        private readonly IChatMessageTypeRepository chatMessageTypeRepository;

        public ChatMessageTypeService(IMapper mapper , IChatMessageTypeRepository chatMessageTypeRepository)
        {
            this.mapper = mapper;
            this.chatMessageTypeRepository = chatMessageTypeRepository;
        }

        public async Task<ChatMessageType> CreateMessageType(ChatMessageType newTypeOfMessage)
        {

            if (newTypeOfMessage != null)
            {
                

                await chatMessageTypeRepository.SaveMessageType(newTypeOfMessage);
                await chatMessageTypeRepository.SaveChanges();
                return newTypeOfMessage;
            }
            return null;

        }

        public async Task<ChatMessageType> UpdateMessageType(int id, ChatMessageType chatMessageType)
        {
            ChatMessageType newUpdatedMessageType = await chatMessageTypeRepository.UpdateMessageTypeById(id, chatMessageType);

            if (newUpdatedMessageType != null)
            {
                await chatMessageTypeRepository.SaveChanges();
                return newUpdatedMessageType;
            }


            return null;

        }


        public async Task DeleteMessageType(int chatMessageTypeId)
        {
            var typeOfMessage = await chatMessageTypeRepository.DeleteMessageTypeById(chatMessageTypeId);
            if (typeOfMessage == null)
            {

                throw new NullReferenceException();

            }

            await chatMessageTypeRepository.SaveChanges();

        }


        public async Task<ChatMessageType> GetMessageType(int chatMessageTypeId)
        {
            var messageType = await chatMessageTypeRepository.GetMessageTypeById(chatMessageTypeId);

            if (messageType == null)
            {
                return null;

            }

            return messageType;

        }
    }
}
