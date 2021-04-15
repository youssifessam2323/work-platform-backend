using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using work_platform_backend.Services;

namespace work_platform_backend.Controllers
{

    [ApiController]
    [Route("api/v1/notification")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService notificationService;
        private readonly UserService userService;

        public NotificationController(NotificationService notificationService, UserService userService)
        {
            this.notificationService = notificationService;
            this.userService = userService;
        }




        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllNotificationByUser()
        {
            var userId = userService.GetUserId();
            return Ok( await notificationService.getAllNotificationByUser(userId));
        }

    }
}