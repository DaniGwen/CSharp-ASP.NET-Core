using System;
using Eventures.Data;
using Eventures.Domein;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Eventures.App.Areas.Identity.IdentityHostingStartup))]
namespace Eventures.App.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<EventuresDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("EventuresDbContextConnection")));

                services.AddDefaultIdentity<EventuresUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<EventuresDbContext>();
            });
        }
    }
}