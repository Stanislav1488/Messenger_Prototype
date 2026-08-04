
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Messenger_Prototype.Hubs
{
    public class ChatHub : Hub
    {
        private static ConcurrentDictionary<string, string> users = new ConcurrentDictionary<string, string>();

        public async Task RegisterUser(string userName)
        {
            users.TryAdd(Context.ConnectionId, userName);
        }

        public async Task Send(string message, string fromUserName, string toUserName)
        {
            var targetUser = users.FirstOrDefault(x => x.Value == toUserName);
            if (targetUser.Key != null)
            {
                await Clients.Client(targetUser.Key).SendAsync("ReceiveMessage", message, fromUserName);
            }
        }
    }
}
