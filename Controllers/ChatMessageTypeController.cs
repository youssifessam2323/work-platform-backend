using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using work_platform_backend.Models;
using work_platform_backend.Services;

namespace work_platform_backend.Controllers
{
    [Route("api/v1/chatMessageType")]
    [ApiController]
    public class ChatMessageTypeController : ControllerBase
    {

        private readonly UserService userService;
        private readonly ChatMessageTypeService chatMessageTypeService;

        public ChatMessageTypeController(ChatMessageTypeService chatMessageTypeService, UserService userService)
        {
            this.chatMessageTypeService = chatMessageTypeService;
            this.userService = userService;

        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> AddMessage(ChatMessageType chatMessageType)
        {
            
           
                var newMessageType = await chatMessageTypeService.CreateMessageType(chatMessageType);
                if (newMessageType != null)
                {
                    return Ok(newMessageType);
                }
                
                return BadRequest("Message type not created !!");
            
        }


        [HttpPut("{MessageTypeId}")]
        public async Task<IActionResult> UpdateMessageType(int MessageTypeId,ChatMessageType chatMessageType)
        {
            ChatMessageType UpdatedMessageType = await chatMessageTypeService.UpdateMessageType(MessageTypeId, chatMessageType);
            if (UpdatedMessageType == null)
            {
                return BadRequest("Message Not Found to Update");
            }
            return Ok(UpdatedMessageType);

        }

        [HttpDelete]
        [Route("{messageChatTypeId}")]
        public async Task<IActionResult> DeletMessage(int messageChatTypeId)
        {
            try
            {
                await chatMessageTypeService.DeleteMessageType(messageChatTypeId);
            }
            catch (DbUpdateException ex)
            {

                return BadRequest("Message Not Found To Delete ");
            }

            return Ok($"MessageType with id = {messageChatTypeId} was  Deleted");
        }


        [HttpGet]
        [Route("{messageTypeId}")]
        public async Task<IActionResult> GetMessageById(int messageTypeId)
        {

            var MessageType = await chatMessageTypeService.GetMessageType(messageTypeId);
            return Ok(MessageType);

        }
    }
}
