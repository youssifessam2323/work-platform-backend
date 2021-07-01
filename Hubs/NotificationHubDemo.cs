using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace work_platform_backend.Hubs
{
    public class NotificationHubDemo : Hub
    {
        public async Task OnConnected()  
        {  
            await base.OnConnectedAsync();
        }  
    }
}