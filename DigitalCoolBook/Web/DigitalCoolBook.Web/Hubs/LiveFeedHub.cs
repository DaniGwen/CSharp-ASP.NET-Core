using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using DigitalCoolBook.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace DigitalCoolBook.App.Hubs
{
    public class LiveFeedHub : Hub
    {
        private readonly INotyfService _toasterService;
        private readonly ILiveFeedService _liveFeedService;
        private readonly UserManager<IdentityUser> _userManager;

        public LiveFeedHub(INotyfService toasterService ,
            ILiveFeedService liveFeedService,
            UserManager<IdentityUser> userManager)
        {
            _toasterService = toasterService;
            _liveFeedService = liveFeedService;
            _userManager = userManager;
        }

        public async Task SendMessage(string userName, string message)
        {
            _toasterService.Information($"Live feed: New Message from {userName}");
            var userId = _userManager.GetUserId(this.Context.User);
            await _liveFeedService.SaveMessage(userId, userName, message);

            await Clients.All.SendAsync("ReceiveMessage", userName, message);
        }

        public async Task GetLiveMessages()
        {
            var messages = (await _liveFeedService.GetLiveMessages()).ToList();

            await Clients.Caller.SendAsync("ReceiveLiveMessages", messages);
        }
    }
}
