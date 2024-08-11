using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using System;

namespace MyProject.Hubs
{
    public class MyHub : Hub
    {
        public async Task SendItemUpdate(string name)
        {
            await Clients.All.SendAsync("ReceiveItemUpdate", name);
        }
    }
}

