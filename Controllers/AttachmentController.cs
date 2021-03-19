using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using work_platform_backend.Models;
using work_platform_backend.Services;


namespace work_platform_backend.Controllers
{
    [Route("api/v1/attachments")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly AttachmentService AttachmentService;
        private  readonly ILogger Logger ; 

        public AttachmentController(AttachmentService attachmentService,ILogger<AttachmentController> logger)
        {
            AttachmentService = attachmentService;
            Logger = logger;

        }

        


        [HttpPost()]
        public async Task<IActionResult> AddAttachment(Attachment attachment)
        {
            var NewAttachment = await AttachmentService.AddAttachment(attachment);
            if (NewAttachment != null)
            {
                return Ok(NewAttachment);
            }
            return BadRequest();
        }

        [HttpPut("{AttachmentId}")]
        public async Task<IActionResult> UpdateProject(int AttachmentId, Attachment attachment)
        {
            Attachment UpdatedAttachment = await AttachmentService.UpdateAttachment(AttachmentId, attachment);
            if (UpdatedAttachment == null)
            {
                return NotFound("There is no attachment with id = " + AttachmentId);
            }
            return Ok(UpdatedAttachment);

        }


        [HttpDelete]
        [Route("{attachmentId}")]
        public async Task<IActionResult> DeletProject(int attachmentId)
        {
            try
            {
                await AttachmentService.DeleteAttachment(attachmentId);


            }
            catch (Exception Ex)
            {

                return NotFound(Ex.Message);
            }

            return Ok($"Object with id = {attachmentId} was  Deleted");
        }



    }
}
