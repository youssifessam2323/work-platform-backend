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
    [Route("api/v1/checkpoints")]
    [ApiController]
    public class CheckPointController : ControllerBase
    {
        private readonly CheckPointService _checkPointService;
        private readonly TaskService taskService ;



        public CheckPointController(CheckPointService checkPointService, TaskService taskService)
        {
            _checkPointService = checkPointService;
            this.taskService = taskService;
        }


        [HttpGet]
        [Route("GetCheckPointsofParentTask/{ParentTaskId}")]
        public async Task<IActionResult> GetCheckPointsofParentTask(int ParentTaskId)
        {

            var GetCheckpointByParentTask = await _checkPointService.GetCheckPointsofParentTask(ParentTaskId);
            if (GetCheckpointByParentTask == null)
            {
                return NotFound();
                
            }
            return Ok(GetCheckpointByParentTask);

        }

        [HttpGet]
        [Route("GetCheckpoint/{checkpointId}")]
        public async Task<IActionResult> GetCheckpoint(int checkpointId)
        {

            var checkPoint = await _checkPointService.GetCheckPoint(checkpointId);
            if (checkPoint == null)
            {
                return NotFound();

            }
            return Ok(checkPoint);

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

    
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCheckPoint(int id, CheckPoint checkPoint)
        {
            CheckPoint UpdatedCheckPoint = await _checkPointService.UpdateCheckPoint(id, checkPoint);
            if (UpdatedCheckPoint == null)
            {
                return NotFound();
            }
            return Ok(UpdatedCheckPoint);

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCheckPoint(int id)
        {
            try
            {
                await _checkPointService.DeleteCheckPoint(id);
                

            }
            catch (Exception Ex)
            {

                return NotFound(Ex.Message);
            }

            return Ok($"Object with id = {id} was  Deleted");
        }

    }

}

