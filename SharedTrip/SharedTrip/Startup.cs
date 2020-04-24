namespace SharedTrip
{
    using SharedTrip.Services;
    using SIS.HTTP;
    using SIS.MvcFramework;
    using SIS.MvcFramework.DependencyContainer;
    using SIS.MvcFramework.Routing;
    using System.Globalization;
    using System.Threading;

    public class Startup : IMvcApplication
    {
        public void Configure(IServerRoutingTable routeTable)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en");

            using(var context = new ApplicationDbContext())
            {
                context.Database.EnsureCreated();
            }
        }

        public void ConfigureServices(IServiceProvider serviceProvider)
        {
            serviceProvider.Add<IUserService, UserService>();
            serviceProvider.Add<ITripService, TripService>();
        }
    }
}
