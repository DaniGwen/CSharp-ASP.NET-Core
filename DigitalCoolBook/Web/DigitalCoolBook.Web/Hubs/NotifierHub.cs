using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalCoolBook.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace DigitalCoolBook.App.Hubs
{
    public class NotifierHub : Hub
    {
        private readonly ITestService _testService;
        private readonly UserManager<IdentityUser> _userManager;

        public NotifierHub(ITestService testService, UserManager<IdentityUser> userManager)
        {
            _testService = testService;
            _userManager = userManager;
        }

        [Authorize]
        public override Task OnConnectedAsync()
        {
            var studentId = _userManager.GetUserId(Context.User);

            var testRoomStudent = _testService.GetTestRoomStudent(studentId);
            if (testRoomStudent != null && !testRoomStudent.Finished)
            {
                //TODO 
            }

            return base.OnConnectedAsync();
        }
        
        public async Task SendNotification(string message, List<string> recipientsIds)
        {
            await Clients.Users(recipientsIds).SendAsync("ReceiveNotification", message);
        }
    }
}
