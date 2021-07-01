using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class ChatMessageService
    {
        private readonly IMapper mapper;
        private readonly IChatMessageRepository chatMessageRepository;

        public ChatMessageService(IMapper mapper , IChatMessageRepository chatMessageRepository)
        {
            this.mapper = mapper;
            this.chatMessageRepository = chatMessageRepository;
        }

        public async Task<ChatMessage> CreateMessage(ChatMessage newMessage,string creatorId,int chatId)
        {

            if (newMessage != null)
            {
                newMessage.Content = Regex.Replace(newMessage.Content, @"(?i)<(?!img|a|/a|/img).*?>", string.Empty);
                newMessage.CreatorId = creatorId;
                newMessage.ChatId = chatId;
                newMessage.Timestamp = DateTime.Now;

                await chatMessageRepository.SaveMessage(newMessage);
                await chatMessageRepository.SaveChanges();
                return newMessage;
            }
            return null;

        }

        public async Task DeleteMessage(int chatMessageId)
        {
            var ChatMessage = await chatMessageRepository.DeleteMessageById(chatMessageId);
            if (ChatMessage == null)
            {

                throw new NullReferenceException();

            }

            await chatMessageRepository.SaveChanges();

        }

        public async Task<bool> DeleteAllMessageByTeamCHat(int ChatId)
        {
            var chatMessage = await chatMessageRepository.DeleteAllMessageByTeamCHat(ChatId);
            if (chatMessage.Count().Equals(0))
            {
                return false;
                
            }

          return await chatMessageRepository.SaveChanges();
        }


        public async Task<ChatMessage> GetMessage(int chatMessageId)
        {
            var chatMessage = await chatMessageRepository.GetMessageById(chatMessageId);

            if (chatMessage == null)
            {
                return null;

            }

            return chatMessage;

        }



        public async Task<IEnumerable<ChatMessage>> GetAllMessagesOfUser(string userId)
        {
            var AllMessages = await chatMessageRepository.GetAllMessageByUser(userId);

            if (AllMessages.Count().Equals(0))
            {
                return null;

            }

            return AllMessages;

        }


        public async Task<IEnumerable<ChatMessage>> GetAllMessagesOfTeamChat(int teamChatId)
        {
            var AllMessages = await chatMessageRepository.GetMessageHistorybyChat(teamChatId);

            if (AllMessages.Count().Equals(0))
            {
                return null;

            }

            return AllMessages;

        }


        public async Task<IEnumerable<ChatMessage>> GetAllMessagesOfMessageType(int messageChatTypeId)
        {
            var AllMessages = await chatMessageRepository.GetAllMessageByMessageType(messageChatTypeId);

            if (AllMessages.Count().Equals(0))
            {
                return null;

            }

            return AllMessages;

        }


       public async Task<IEnumerable<ChatMessage>> GetMessageHistorybyChat(int chatId)

        {
            var chatMessagesHistory = await chatMessageRepository.GetMessageHistorybyChat(chatId);
            if (chatMessagesHistory.Count().Equals(0))
            {
                return null;

            }

            return chatMessagesHistory;
        }






    }
}
