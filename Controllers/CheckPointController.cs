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
        private readonly CheckPointService checkpointService;
        private readonly TaskService taskService ;



        public CheckPointController(CheckPointService checkPointService, TaskService taskService)
        {
            checkpointService = checkPointService;
            this.taskService = taskService;
        }


        [HttpGet]
        [Route("parenttask/{parentTaskId}")]
        public async Task<IActionResult> GetCheckPointsofParentTask(int parentTaskId)
        {
            try
            {
            var getCheckpointByParentTask = await checkpointService.GetCheckPointsofParentTask(parentTaskId);
             return Ok(getCheckpointByParentTask);
            }
            catch(Exception e )
            {
                return NotFound(e.Message);
            }   

        }

        [HttpGet]
        [Route("{checkpointId}")]
        public async Task<IActionResult> GetCheckpoint(int checkpointId)
        {
            try
            {
               var checkPoint = await checkpointService.GetCheckPoint(checkpointId);
                return Ok(checkPoint);
            }
            catch(Exception e )
            {
                return NotFound(e.Message);
            }   

         

        }


        [HttpGet]
        [Route("{CheckPointId}/subtasks")]
        public async Task<IActionResult> GetSubTasksOfParentCheckPoint(int checkPointId)
        {
            try
            { 
                return Ok(await taskService.GetSubTasksByParentCheckPoint(checkPointId));
            }
            catch(Exception e )
            {
                return NotFound(e.Message);
            }   

        }

    
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCheckPoint(int id, CheckPoint checkPoint)
        {
            CheckPoint UpdatedCheckPoint = await checkpointService.UpdateCheckPoint(id, checkPoint);
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
                await checkpointService.DeleteCheckPoint(id);
                

            }
            catch (Exception Ex)
            {

                return NotFound(Ex.Message);
            }

            return Ok($"Object with id = {id} was  Deleted");
        }

    }

}

