using System.Collections.Generic;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public interface INotificationRepository
    {
        Task<List<Notification>> getNotificationByUser(string userId);
        Task<Notification> CreateNewNotification(Notification notification);
    }
}