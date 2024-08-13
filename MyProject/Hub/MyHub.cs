using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

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
