using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.SignalR;

namespace DigitalCoolBook.App.Hubs
{
    public class LiveFeedHub : Hub
    {
        private readonly INotyfService _toasterService;

        public LiveFeedHub(INotyfService toasterService)
        {
            _toasterService = toasterService;
        }
        public async Task SendMessage(string user, string message)
        {
            _toasterService.Information($"Live feed: New Message from {user}");

            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
