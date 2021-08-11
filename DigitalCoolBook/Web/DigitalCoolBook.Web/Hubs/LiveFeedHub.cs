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

        public async Task SendMessage(string user, string message)
        {
            _toasterService.Information($"Live feed: New Message from {user}");
            await Task.Delay(1000);
            var teacherId = _userManager.GetUserId(this.Context.User);
            _liveFeedService.SaveMessage(teacherId, message);

            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
