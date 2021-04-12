
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using work_platform_backend.Dtos;
using work_platform_backend.Models;
using work_platform_backend.Services;


namespace work_platform_backend.Controllers
{
    [Route("api/v1/attachments")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly AttachmentService attachmentService;
        private  readonly ILogger Logger ; 
        private  readonly UserService userService;

        public AttachmentController(AttachmentService attachmentService, ILogger<AttachmentController> logger, UserService userService)
        {
            this.attachmentService = attachmentService;
            Logger = logger;
            this.userService = userService;
        }



        ///<summary>
        /// Add new attachment (taskId is required)
        ///</summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [HttpPost]
        public async Task<IActionResult> AddAttachment(AttachmentDto attachment)
        {
                await attachmentService.AddAttachment(attachment,userService.GetUserId());
                return Ok();
        }

        [HttpPut("{AttachmentId}")]
        public async Task<IActionResult> UpdateAttachment(int AttachmentId, Attachment attachment)
        {
            Attachment UpdatedAttachment = await attachmentService.UpdateAttachment(AttachmentId, attachment);
            if (UpdatedAttachment == null)
            {
                return NotFound("There is no attachment with id = " + AttachmentId);
            }
            return Ok(UpdatedAttachment);

        }

        ///<summary>
        /// delete attachment by it's Id 
        ///</summary>
        ///<param name="attachmentId"></param>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [HttpDelete]
        [Route("{attachmentId}")]
        public async Task<IActionResult> DeletAttachment(int attachmentId)
        {
            try
            {   await attachmentService.getAttachmentById(attachmentId);
                await attachmentService.DeleteAttachment(attachmentId);
            }
            catch (Exception Ex)
            {

                return NotFound(Ex.Message);
            }

            return Ok("attachment deleted successfully");
        }



    }
}
