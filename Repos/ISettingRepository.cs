using System.Collections.Generic;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public interface ISettingRepository
    {
        Task<IEnumerable<Setting>> GellAllSettings();
        Task<IEnumerable<Setting>> GetAllSettingsByRoom(int roomId);
        Task SaveSetting(Setting setting);
        Task<Setting> UpdateSettingById(int settingId,Setting setting);
        Task<Setting> DeleteSettingById( int settingId);
        Task<RoomSettings> RemoveSettingfromRoom(int roomId, int settingId);
        Task<bool> SaveChanges();
    }
}