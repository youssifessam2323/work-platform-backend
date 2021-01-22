using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;
using work_platform_backend.Services;

namespace work_platform_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly AttachmentService _attachmentService;

        public AttachmentController(AttachmentService attachmentService)
        {
            _attachmentService = attachmentService;

        }

        [HttpGet]
        [Route("GetAttachmentsInTask/{TaskId}")]
        public async Task<IActionResult> GetAttachmentsInTask(int TaskId)
        {

            var Attachments = await _attachmentService.GetAttachmentsOfTask(TaskId);
            if (Attachments == null)
            {
                return NotFound();

            }
            return Ok(Attachments);

        }


        [HttpPost("AddAttachment")]
        public async Task<IActionResult> AddAttachment(Attachment attachment)
        {
            var NewAttachment = await _attachmentService.AddAttachment(attachment);
            if (NewAttachment != null)
            {
                return Ok(NewAttachment);
            }
            return BadRequest();
        }

        [HttpPut("{AttachmentId}")]
        public async Task<IActionResult> UpdateProject(int AttachmentId, Attachment attachment)
        {
            Attachment UpdatedAttachment = await _attachmentService.UpdateAttachment(AttachmentId, attachment);
            if (UpdatedAttachment == null)
            {
                return NotFound();
            }
            return Ok(UpdatedAttachment);

        }


        [HttpDelete]
        [Route("delete/{attachmentId}")]
        public async Task<IActionResult> DeletProject(int attachmentId)
        {
            try
            {
                await _attachmentService.DeleteAttachment(attachmentId);


            }
            catch (Exception Ex)
            {

                return NotFound(Ex.Message);
            }

            return Ok($"Object with id = {attachmentId} was  Deleted");
        }



    }
}
