using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public class SettingRepo : ISettingRepository
    {
        private readonly ApplicationContext _context;

        public SettingRepo(ApplicationContext context)
        {
            _context = context;
        }   

        public async Task<IEnumerable<Setting>> GellAllSettings()
        {
            return (await _context.Settings.ToListAsync());
        }

        public async Task<IEnumerable<Setting>> GetAllSettingsByRoom(int roomId)
        {
            var AllSettingOfRoom = (from Room in _context.Rooms
                                 join RoomSetting in _context.RoomSettings
                                 on Room.Id equals RoomSetting.RoomId
                                 join Setting in _context.Settings
                                 on RoomSetting.SettingId equals Setting.Id

                                 where RoomSetting.RoomId == roomId
                                 select Setting
                  ).ToListAsync();

            return await AllSettingOfRoom;
        }

        

        public async Task SaveSetting(Setting setting )
        {
           await _context.AddAsync(setting);
        }

        public async Task<Setting> UpdateSettingById(int settingId, Setting setting)
        {
            var NewSetting = await _context.Settings.FindAsync(settingId);
            if (NewSetting != null)
            {
                NewSetting.Name = setting.Name;
                return NewSetting;

            }
            return null;



        }

        public async Task<Setting> DeleteSettingById(int settingId)
        {
            Setting setting = await _context.Settings.FindAsync(settingId);
            if (setting != null)
            {
                _context.Settings.Remove(setting);

            }
            return setting;
        }



        public async Task<RoomSettings> RemoveSettingfromRoom(int roomId,int settingId)
        {
            RoomSettings roomSettings = await _context.RoomSettings.Where(rs => rs.RoomId == roomId && rs.SettingId==settingId)
                                                    .SingleOrDefaultAsync();
            if (roomSettings != null)
            {
                _context.RoomSettings.Remove(roomSettings);

            }
            return roomSettings;
            
        }



        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
