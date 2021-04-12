using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using work_platform_backend.Models;
using work_platform_backend.Services;

namespace work_platform_backend.Controllers
{
    [ApiController]
    [Route("api/v1/comments")]
    public class CommentController: ControllerBase
    {
        public readonly CommentService commentService;

        public CommentController(CommentService commentService)
        {
            this.commentService = commentService;
        }

        //not tested
        [HttpPost]
        [Route("{commentId}/replies")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> SaveNewCheckpointInTask(int commentId,Comment comment)
        {
            return Ok(await commentService.AddNewReplyToComment(commentId,comment));
        }


       
        [HttpDelete]
        [Route("{commentId}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            try
            {
                await commentService.DeleteComment(commentId);
            }
            catch (Exception Ex)
            {

                return NotFound(Ex.Message);
            }

            return Ok("Comment deleted successfully");
        }

    }
}