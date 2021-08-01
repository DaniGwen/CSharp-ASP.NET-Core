namespace DigitalCoolBook.App.Hubs
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using DigitalCoolBook.Models;
    using DigitalCoolBook.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;

    public class TestHub : Hub
    {
        private readonly IUserService _userService;

        public TestHub(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        public override async Task OnConnectedAsync()
        {
            var userName = await this.GetUserNameAsync();

            if (userName != null)
                await this.Clients.All.SendAsync("OnConnected", $"{userName} test started");
        }

        [Authorize]
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userName = await this.GetUserNameAsync();

            await _userService.SetStudentFinishedTestRoom(userName);

            if (userName != null)
                await this.Clients.All.SendAsync("OnDisconnected", $"{userName} test submitted", userName);
        }

        private async Task<string> GetUserNameAsync()
        {
            var userId = this.Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var identityUser = await _userService.GetUserAsync(userId);

            if (this.Context.User.IsInRole("Student"))
            {
                var student = (Student)identityUser;

                return student.Name;
            }

            return null;
        }
    }
}
