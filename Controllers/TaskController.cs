using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using work_platform_backend.Dtos;
using work_platform_backend.Exceptions;
using work_platform_backend.Exceptions.Task;
using work_platform_backend.Models;
using work_platform_backend.Services;

namespace work_platform_backend.Controllers
{
    [Route("api/v1/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskService taskService;
        private readonly AttachmentService AttachmentService;
        private readonly CommentService commentService;
        private readonly UserService userService;
        private readonly CheckPointService checkPointService;
        private readonly SessionService sessionService;

        public TaskController(TaskService taskService, AttachmentService attachmentService, CommentService commentService, UserService userService, CheckPointService checkPointService, SessionService sessionService)
        {
            this.taskService = taskService;
            AttachmentService = attachmentService;
            this.commentService = commentService;
            this.userService = userService;
            this.checkPointService = checkPointService;
            this.sessionService = sessionService;
        }




       

        ///<summary>
        /// get task's attachments
        ///</summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(List<AttachmentDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)] 
        [HttpDelete("{roomId}")]
        [HttpGet]
        [Route("{TaskId}/attachments")]
        public async Task<IActionResult> GetAttachmentsInTask(int TaskId)
        {
            try
            {
            var Attachments = await AttachmentService.GetAttachmentsOfTask(TaskId);
            return Ok(Attachments);
            }
            catch(TaskNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        [Route("GetTasksOfTeam/{TeamId}")]
        public async Task<IActionResult> GetTasksOfTeam(int TeamId)
        {

            var TasksByTeam = await taskService.GetTasksByTeam(TeamId);
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

            var TasksByProject = await taskService.GetTasksByProject(ProjectId);
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

            var TasksByParentCheckpoint = await taskService.GetSubTasksByParentCheckPoint(CheckPointId);
            if (TasksByParentCheckpoint == null)
            {
                return NotFound();

            }
            return Ok(TasksByParentCheckpoint);

        }

  


        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("{TaskId}/dependants")]
        public async Task<IActionResult> GetTaskDependantTasks(int TaskId)
        {
            var dependantTasks = await taskService.GetTaskDependantTasks(TaskId);
            return Ok(dependantTasks);
        }


        [HttpGet]
        [Route("{TaskId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetSingleTask(int TaskId)
        {

            var task = await taskService.GetTask(TaskId);
            if (task == null)
            {
                return NotFound();

            }
            return Ok(task);

        }






        [HttpPut("{TaskId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateTask(int TaskId, RTask task)
        {
            RTask UpdatedTask = await taskService.UpdateTask(TaskId, task);
            if (UpdatedTask == null)
            {
                return NotFound();
            }
            return Ok(UpdatedTask);

        }



        //not working
        [HttpDelete]
        [Route("{TaskId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeletTask(int TaskId)
        {
            try
            {
                await taskService.DeleteTask(TaskId);
            }
            catch (Exception Ex)
            {

                return NotFound(Ex.Message);
            }

            return Ok($"Object with id = {TaskId} was  Deleted");
        }

        [HttpPost]
        [Route("{taskId}/comments")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> SaveNewCheckpointInTask(int taskId,Comment comment)
        {
            return Ok(await commentService.AddNewCommentInTask(userService.GetUserId(),taskId,comment));
        }


        [HttpGet]
        [Route("{taskId}/comments")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetTaskComments(int taskId)
        {
            return Ok(await commentService.GetCommentsByTask(taskId));
        }



    
        [HttpGet]
        [Route("{taskId}/checkpoints")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetTaskCheckpoints(int taskId)
        {
            return Ok(await checkPointService.GetCheckpointsByTask(taskId));
        }

        [HttpPost]
        [Route("{taskId}/checkpoints")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> SaveNewCheckpointInTask(int taskId,CheckPoint checkPoint)
        {
            return Ok(await checkPointService.SaveNewCheckpointInTask(taskId,checkPoint));
        }


        [HttpGet]
        [Route("{taskId}/authuser/sessions")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetTaskAuthUserSessions(int taskId)
        {
            return Ok(await sessionService.GetSessionsByTaskAndUser(userService.GetUserId(),taskId));
        }

        [HttpGet]
        [Route("{taskId}/assignedusers")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetAssignUsersForATask(int taskId)
        {
            return Ok(await taskService.GetUsersAssignedToTaskByTaskId(taskId));
        }



       
    }
}
