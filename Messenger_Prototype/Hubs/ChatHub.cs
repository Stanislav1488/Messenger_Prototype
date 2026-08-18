
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Messenger_Prototype.Hubs
{
    public class ChatHub : Hub
    {
        private static ConcurrentDictionary<string, string> users = new ConcurrentDictionary<string, string>();

        private static List<string> onlineUsers = new List<string>();

        public async Task UserConnected(string userName)
        {
            if (!onlineUsers.Contains(userName))
            {
                onlineUsers.Add(userName);
            }
            var userList = onlineUsers.ToList();
            await Clients.All.SendAsync("UpdateOnlineUsers", userList);
        }

        //Исключения из списка
        //public async Task UserDisconnected(string userName)
        //{
        //    if (onlineUsers.Contains(userName))
        //    {
        //        onlineUsers.Remove(userName);
        //    }

        //    var userList = onlineUsers.ToList();
        //    await Clients.All.SendAsync("UpdateOnlineUsers", userList);
        //}

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
