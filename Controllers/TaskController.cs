using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;
using work_platform_backend.Services;

namespace work_platform_backend.Controllers
{
    [Route("api/v1/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;
        private readonly AttachmentService AttachmentService;
        private readonly CommentService commentService;

        public TaskController(TaskService taskService, AttachmentService attachmentService,CommentService commentService)
        {
            _taskService = taskService;
            AttachmentService = attachmentService;
            this.commentService = commentService;
        }


        [HttpGet]
        [Route("GetTasksOfCreator/{TaskCreatorId}")]
        public async Task<IActionResult> GetTasksOfCreator(string TaskCreatorId)
        {

            var TaskByCreator = await _taskService.GetTaskByCreator(TaskCreatorId);
            if (TaskByCreator == null)
            {
                return NotFound();

            }
            return Ok(TaskByCreator);

        }


        [HttpGet]
        [Route("{TaskId}/attatchments")]
        public async Task<IActionResult> GetAttachmentsInTask(int TaskId)
        {

            var Attachments = await AttachmentService.GetAttachmentsOfTask(TaskId);
            if (Attachments == null)
            {
                return NotFound();

            }
            return Ok(Attachments);

        }

        [HttpGet]
        [Route("GetTasksOfTeam/{TeamId}")]
        public async Task<IActionResult> GetTasksOfTeam(int TeamId)
        {

            var TasksByTeam = await _taskService.GetTasksByTeam(TeamId);
            if (TasksByTeam == null)
            {
                return NotFound();

            }
            return Ok(TasksByTeam);

        }

        [HttpGet]
        [Route("GetTasksOfProject/{ProjectId}")]
        public async Task<IActionResult> GetTasksOfProject(int ProjectId)
        {

            var TasksByProject = await _taskService.GetTasksByProject(ProjectId);
            if (TasksByProject == null)
            {
                return NotFound();

            }
            return Ok(TasksByProject);

        }

        [HttpGet]
        [Route("GetSubTasksOfParentCheckPoint/{CheckPointId}")]
        public async Task<IActionResult> GetSubTasksOfParentCheckPoint(int CheckPointId)
        {

            var TasksByParentCheckpoint = await _taskService.GetSubTasksByParentCheckPoint(CheckPointId);
            if (TasksByParentCheckpoint == null)
            {
                return NotFound();

            }
            return Ok(TasksByParentCheckpoint);

        }

        [HttpGet]
        [Route("GetTask/{TaskId}")]
        public async Task<IActionResult> GetSingleTask(int TaskId)
        {

            var task = await _taskService.GetTask(TaskId);
            if (task == null)
            {
                return NotFound();

            }
            return Ok(task);

        }


        [HttpPost()]
        public async Task<IActionResult> AddTask(RTask task)
        {
            var NewTask = await _taskService.AddTask(task);
            if (NewTask != null)
            {
                return Ok(NewTask);
            }
            return BadRequest();
        }

        [HttpPut("{TaskId}")]
        public async Task<IActionResult> UpdateTask(int TaskId, RTask task)
        {
            RTask UpdatedTask = await _taskService.UpdateTask(TaskId, task);
            if (UpdatedTask == null)
            {
                return NotFound();
            }
            return Ok(UpdatedTask);

        }


        [HttpDelete]
        [Route("delete/{TaskId}")]
        public async Task<IActionResult> DeletTask(int TaskId)
        {
            try
            {
                await _taskService.DeleteTask(TaskId);


            }
            catch (Exception Ex)
            {

                return NotFound(Ex.Message);
            }

            return Ok($"Object with id = {TaskId} was  Deleted");
        }

        [HttpGet]
        [Route("{taskId}/comments")]
        public async Task<IActionResult> GetTaskComments(int taskId)
        {
             return Ok(await commentService.GetCommentsByTask(taskId));
        }


        [HttpPost]
        [Route("{taskId}/comments")]
        public async Task<IActionResult> AddComment(int taskId,[FromBody]Comment comment)
        {
            var newComment = await commentService.CreatNewComment(taskId,comment);
            
                if(newComment != null )
                    return Ok();

                    
            return BadRequest();
        }
    }
}
