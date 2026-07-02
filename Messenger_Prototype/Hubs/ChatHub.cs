
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Messenger_Prototype.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(string message, string userName)
        {
            await Clients.All.SendAsync("ReceiveMessage", message, userName);
        }
    }
}
