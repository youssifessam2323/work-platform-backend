using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationContext context;

        public NotificationRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<List<Notification>> getNotificationByUser(string userId)
        {
            return await context.Notification.Where(n => n.UserId == userId).ToListAsync();
        }
    }
}