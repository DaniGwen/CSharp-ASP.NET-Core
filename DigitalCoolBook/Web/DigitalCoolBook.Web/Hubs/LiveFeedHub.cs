using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DigitalCoolBook.App.Hubs
{
    public class LiveFeedHub : Hub
    {
        public async Task SendMessage(string user, string message,)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
