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
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
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


        [HttpPost("AddTask")]
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
    }
}
