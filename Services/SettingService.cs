using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class SettingService
    {
        private readonly ISettingRepository _settingRepository;

        public SettingService(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }

        public async Task<Setting> AddSetting(Setting newSetting)
        {
            if (newSetting != null)
            {
                await _settingRepository.SaveSetting(newSetting);
                await _settingRepository.SaveChanges();
                return newSetting;
            }
            return null;

        }

        public async Task<Setting> UpdateSetting(int settingId, Setting setting)
        {
            Setting UpdatedSetting = await _settingRepository.UpdateSettingById(settingId, setting);

            if (UpdatedSetting != null)
            {
                await _settingRepository.SaveChanges();
                return UpdatedSetting;
            }

            return null;

        }


        public async Task DeleteSetting(int settingId)
        {
            var Team = await _settingRepository.DeleteSettingById(settingId);
            if (Team == null)
            {

                throw new NullReferenceException();

            }

            await _settingRepository.SaveChanges();

        }


        public async Task<IEnumerable<Setting>> GetSettings()
        {
            var settings = await _settingRepository.GellAllSettings();

            if (settings.Count().Equals(0))
            {
                return null;

            }

            return settings;

        }

        public async Task<IEnumerable<Setting>> GetSettingsOfRoom(int roomId)
        {
            var RoomSettings = await _settingRepository.GetAllSettingsByRoom(roomId);

            if (RoomSettings.Count().Equals(0))
            {
                return null;

            }

            return RoomSettings;

        }
    }
}
