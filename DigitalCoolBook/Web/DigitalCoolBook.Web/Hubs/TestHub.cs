﻿namespace DigitalCoolBook.App.Hubs
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
        private readonly IUserService userService;

        public TestHub(IUserService userService)
        {
            this.userService = userService;
        }

        [Authorize]
        public async override Task OnConnectedAsync()
        {
            var userName = await this.GetUserNameAsync();

            if (userName != null)
                await this.Clients.All.SendAsync("OnConnected", $"{userName} connected");
        }

        [Authorize]
        public async override Task OnDisconnectedAsync(Exception exception)
        {
            var userName = await this.GetUserNameAsync();

            await this.userService.SetStudentFinishedTestRoom(userName);

            if (userName != null)
                await this.Clients.All.SendAsync("OnDisconnected", $"{userName} disconected", userName);
        }

        private async Task<string> GetUserNameAsync()
        {
            var userId = this.Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var identityUser = await this.userService.GetUserAsync(userId);

            if (this.Context.User.IsInRole("Student"))
            {
                var student = (Student)identityUser;

                return student.Name;
            }

            return null;
        }
    }
}
