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
    public class SettingController : ControllerBase
    {
        private readonly SettingService _settingService;

        public SettingController(SettingService settingService)
        {
            _settingService = settingService;
        }
        [HttpGet]
        [Route("GetSettings")]
        public async Task<ActionResult<IEnumerable<Setting>>> GetSettings()
        {
            var AllSetting = await _settingService.GetSettings();
            if (AllSetting != null)
            {
                return Ok(AllSetting);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("GetSettingsInRoom/{roomId}")]

        public async Task<ActionResult<IEnumerable<Setting>>> GetAllRoomSetting(int roomId)
        {
            var AllSettingInRoom = await _settingService.GetSettingsOfRoom(roomId);
            if (AllSettingInRoom != null)
            {
                return Ok(AllSettingInRoom);
            }
            return NotFound();
        }


        [HttpPost]
        [Route("AddSetting")]
        public async Task<ActionResult<Setting>> AddNewSetting(Setting Newsetting)
        {
            var Setting = await _settingService.AddSetting(Newsetting);

            if (Setting == null)
            {
                return NotFound();
            }

            return Ok(Setting);
        }

        [HttpPut]
        [Route("UpdateSetting/{settingId}")]
        public async Task<IActionResult> UpdateSetting(int settingId, Setting setting)
        {
            Setting UpdatedSetting = await _settingService.UpdateSetting(settingId, setting);
            if (UpdatedSetting == null)
            {
                return NotFound();
            }
            return Ok(UpdatedSetting);
        }




        [HttpDelete]
        [Route("DeleteSetting/{settingId}")]
        public async Task<ActionResult<Permission>> DeletePermission(int settingId)
        {
            try
            {
                await _settingService.DeleteSetting(settingId);


            }
            catch (Exception Ex)
            {

                return NotFound(Ex.Message);
            }

            return Ok($"Object with id = {settingId} was  Deleted");
        }


    }
}
