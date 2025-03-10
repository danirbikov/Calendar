﻿using Microsoft.AspNetCore.SignalR;

namespace Calendar.Jobs
{
    public class NotificationSignalRHub : Hub
    {
        public async Task SendNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}
