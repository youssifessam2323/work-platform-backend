using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using work_platform_backend.Hubs;
using work_platform_backend.Services;

namespace work_platform_backend.Controllers
{

    
    [Route("api/v1/notifications")]
    [ApiController]
    public class SignalRDemoController
    {
       private readonly IHubContext<NotificationHubDemo> _hub;
        private readonly UserService userService;

        public SignalRDemoController(IHubContext<NotificationHubDemo> hub, UserService userService)
        {
            _hub = hub;
            this.userService = userService;
        }

       [HttpGet]
       [Authorize(AuthenticationSchemes = "Bearer")]
       public async Task Send(string message)
       {
           Console.WriteLine("user Id" + userService.GetUserId());
           await _hub.Clients.User(userService.GetUserId()).SendAsync("ReceiveMessage", message);

       }

       [HttpGet("/sendToUser")]
       [Authorize(AuthenticationSchemes = "Bearer")]
       public Task DirectMessage(string userId, string message)
        {
            return _hub.Clients.User(userId).SendAsync("ReceiveMessage", userId, message);
        }   

        
       
    }
}