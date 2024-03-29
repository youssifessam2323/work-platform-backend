﻿using Microsoft.AspNetCore.Authorization;
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
        [HttpGet]
        [Route("{taskId}/attachments")]
        public async Task<IActionResult> GetAttachmentsInTask(int TaskId)
        {
            try
            {
            var attachments = await AttachmentService.GetAttachmentsOfTask(TaskId);
            return Ok(attachments);
            }
            catch(TaskNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }


        ///<summary>
        /// get task's dependant tasks
        ///</summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(List<TaskDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)] 
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("{taskId}/dependants")]
        public async Task<IActionResult> GetTaskDependantTasks(int taskId)
        {
            try
            {
                var dependantTasks = await taskService.GetTaskDependantTasks(taskId);
                return Ok(dependantTasks);

            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }

          ///<summary>
        /// get task by Id
        ///</summary>
        ///<param name="taskId"></param>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(List<TaskDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)] 
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
         
        [HttpGet]
        [Route("{taskId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetSingleTask(int taskId)
        {
            try
            {
            var task = await taskService.GetTask(taskId);
            return Ok(task);
            }catch(Exception e)
            {
                return NotFound(e.Message);
            }

        }

        ///<summary>
        /// add new subtask from a specific checkpoint 
        ///</summary>
        ///<param name="parentCheckpointId"></param>
        ///<param name="task"></param>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(List<TaskDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)] 
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [HttpPost("subtask/{parentCheckpointId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddSubtaskToCheckpoint(int parentCheckpointId, RTask task)
        {
            try
            {
               
                return Ok( await taskService.AddNewSubTask(userService.GetUserId(),parentCheckpointId, task));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }



        // Not Working
        [HttpPut("{taskId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateTask(int taskId, RTask task)
        {
            try
            {
            await taskService.UpdateTask(taskId, task);
            return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }



        // //not working
        // [HttpDelete]
        // [Route("{TaskId}")]
        // //[Authorize(AuthenticationSchemes = "Bearer")]
        // public async Task<IActionResult> DeleteTask(int TaskId)
        // {
        //     try
        //     {
        //        if( await taskService.DeleteTask(TaskId))
        //         {
        //             return Ok($"Object with id = {TaskId} was  Deleted");
        //         }
        //         throw new Exception("Error nUll");
        //     }
        //     catch (Exception Ex)
        //     {

        //         return NotFound(Ex.Message);
        //     }
        // }



        ///<summary>
        /// save new comment in task
        ///</summary>
        ///<param name="taskId"></param>
        ///<param name="commentDto"></param>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)] 
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [HttpPost]
        [Route("{taskId}/comments")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> SaveNewCommentInTask(int taskId,CommentDto commentDto)
        {
            try
            {
                await commentService.AddNewCommentInTask(userService.GetUserId(),taskId,commentDto);
                return Ok();
            }
            catch(Exception e )
            {
                return NotFound(e.Message);
            }
        }

        ///<summary>
        /// get comments of specific task
        ///</summary>
        ///<param name="taskId"></param>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(List<CommentDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)] 
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [HttpGet]
        [Route("{taskId}/comments")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetTaskComments(int taskId)
        {
            try
            {
                return Ok(await commentService.GetCommentsByTask(taskId));
            }
            catch(Exception e )
            {
                return NotFound(e.Message);
            }
            
        }



        ///<summary>
        /// get checkpoints of specific task
        ///</summary>
        ///<param name="taskId"></param>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(List<CheckPointDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)] 
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [HttpGet]
        [Route("{taskId}/checkpoints")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetTaskCheckpoints(int taskId)
        {
             try
            {
                return Ok(await checkPointService.GetCheckpointsByTask(taskId));
            }
            catch(Exception e )
            {
                return NotFound(e.Message);
            }
        }



        ///<summary>
        /// add new  checkpoint to specific task
        ///</summary>
        ///<param name="taskId"></param>
        ///<param name="checkPointDto"></param>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(void),(int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)] 
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [HttpPost]
        [Route("{taskId}/checkpoints")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> SaveNewCheckpointInTask(int taskId,CheckPointDto checkPointDto)
        {
            try
            {
               return Ok(await checkPointService.SaveNewCheckpointInTask(taskId,checkPointDto));
            }
            catch(Exception e )
            {
                return NotFound(e.Message);
            }   

        }

        ///<summary>
        /// get session of the task and the current auth user
        ///</summary>
        ///<param name="taskId"></param>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(void),(int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [HttpGet]
        [Route("{taskId}/authuser/sessions")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetTaskAuthUserSessions(int taskId)
        {
            return Ok(await sessionService.GetSessionsByTaskAndUser(userService.GetUserId(),taskId));
        }



        ///<summary>
        /// get assigned user for a task
        ///</summary>
        ///<param name="taskId"></param>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(List<UserDto>),(int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)] 
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [HttpGet]
        [Route("{taskId}/assignedusers")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetAssignUsersForATask(int taskId)
        {
            return Ok(await taskService.GetUsersAssignedToTaskByTaskId(taskId));
        }



        ///<summary>
        ///  assigned user for a task
        ///</summary>
        ///<param name="taskId"></param>
        ///<param name="users"></param>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(List<UserDto>),(int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)] 
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [HttpPost]
        [Route("{taskId}/assignedusers")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AssignUsersForATask(int taskId,[FromBody] List<string> users)
        {
            try
            {
                await taskService.AssignedUsersToTaskByTaskId(taskId, users);
            }catch(Exception e)
            {
                return NotFound(e);
            }
            return Ok();
        }


        
        ///<summary>
        ///  remove assigned user/s from a task
        ///</summary>
        ///<param name="taskId"></param>
        ///<param name="users"></param>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(List<UserDto>),(int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)] 
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [HttpDelete]
        [Route("{taskId}/assignedusers")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> RemoveAssignUsersForATask(int taskId,[FromBody] List<string> users)
        {
            try
            {
                await taskService.RemoveAssignedUsersToTaskByTaskId(taskId, users);
            }catch(Exception e)
            {
                return NotFound(e);
            }
            return Ok();
        }

       
    }
}
