using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using work_platform_backend.Models;
using work_platform_backend.Services;

namespace work_platform_backend.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly ApplicationContext context;
        private readonly UserService userService;

        public NotificationHub(UserService userService, ApplicationContext context)
        {
            this.userService = userService;
            this.context = context;
        }

        

        // public Task PushNotification()
        // {
            
        // }
    }
}