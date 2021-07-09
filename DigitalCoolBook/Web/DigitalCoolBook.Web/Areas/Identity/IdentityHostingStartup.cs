using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(DigitalCoolBook.App.Areas.Identity.IdentityHostingStartup))]
namespace DigitalCoolBook.App.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}