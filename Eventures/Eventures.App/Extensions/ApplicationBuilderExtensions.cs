using Eventures.App.Areas.Identity.Data.Seeding;
using Eventures.App.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Eventures.App.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseDatabaseSeeding(this IApplicationBuilder app)
        {
            //:TODO Seed root user
        }
    }
}
