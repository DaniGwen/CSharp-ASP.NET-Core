using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Panda.Data;
using Panda.Domein;
using System.Linq;

namespace Panda.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PandaDbContext>(options =>
                 options
                 .UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<PandaUser, PandaUserRole>()
                .AddEntityFrameworkStores<PandaDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                //Password settings
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
            });

            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<PandaDbContext>())
                {
                    context.Database.EnsureCreated();

                    if (!context.Roles.Any())
                    {
                        context.Roles.Add(new PandaUserRole { Name = "Admin", NormalizedName = "ADMIN" });
                        context.Roles.Add(new PandaUserRole { Name = "User", NormalizedName = "USER" });
                    }


                    if (!context.PackageStatuses.Any())
                    {
                        context.PackageStatuses.Add(new PackageStatus { Name = "Pending" });
                        context.PackageStatuses.Add(new PackageStatus { Name = "Shipped" });
                        context.PackageStatuses.Add(new PackageStatus { Name = "Delivered" });
                        context.PackageStatuses.Add(new PackageStatus { Name = "Acquired" });
                    }

                    context.SaveChanges();
                }
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseDeveloperExceptionPage();

            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}
