using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class NotificationService
    {
        private readonly INotificationRepository notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            this.notificationRepository = notificationRepository;
        }

        public async Task<List<Notification>> getAllNotificationByUser(string userId)
        {
            return await notificationRepository.getNotificationByUser(userId);   
        }
    }
}