using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using work_platform_backend.Models;
using work_platform_backend.Services;

namespace work_platform_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {    
        private readonly PermissionService _permissionService;

        public PermissionController(PermissionService permissionService)
        {
            _permissionService = permissionService;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Permission>>> GetPermissions()
        {
           var AllPermission =  await _permissionService.GetPermissions();
            if(AllPermission!=null)
            {
                return Ok(AllPermission);
            }
            return NotFound();
        }

        
        [HttpGet]
        [Route("getPermission/{permissionId}")]
        public async Task<ActionResult<Permission>> GetPermissionById(int permissionId)
        {
            var Permission = await _permissionService.GetPermissionById(permissionId);

            if (Permission == null)
            {
                return NotFound();
            }

            return Ok(Permission);
        }



       
        [HttpGet]
        [Route("getPermissionString/{permissionName}")]
        public async Task<ActionResult<Permission>> GetPermissionByName(string permissionName)
        {
            var Permission = await _permissionService.GetPermissionByName(permissionName);

            if (Permission == null)
            {
                return NotFound();
            }

            return Ok(Permission);
        }




        [HttpPost]
        public async Task<ActionResult<Permission>> AddNewPermission(Permission permission)
        {
            var Permission = await _permissionService.AddPermission(permission);

            if (Permission == null)
            {
                return NotFound();
            }

            return Ok(Permission);
        }

        [HttpPut("{permissionId}")]
        public async Task<IActionResult> UpdatePermission(int permissionId, Permission permission)
        {
            Permission UpdatedPermission = await _permissionService.UpdatePermission(permissionId, permission);
            if (UpdatedPermission == null)
            {
                return NotFound();
            }
            return Ok(UpdatedPermission);
        }

     

       
        [HttpDelete("{permissionId}")]
        public async Task<ActionResult<Permission>> DeletePermission(int permissionId)
        {
            try
            {
                await _permissionService.DeletePermission(permissionId);


            }
            catch (Exception Ex)
            {

                return NotFound(Ex.Message);
            }

            return Ok($"Object with id = {permissionId} was  Deleted");
        }

       
    }
}
