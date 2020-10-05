using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using KniveGallery.Web.Data;
using KniveGallery.Web.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace KniveGallery.Web
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();
            services.AddControllersWithViews();
            services.AddRazorPages();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = false;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedEmail = false;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                using (var serviceScope = app.ApplicationServices.CreateScope())
                {
                    using (var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        context.Database.EnsureDeletedAsync().Wait();
                        context.Database.EnsureCreatedAsync().Wait();

                        if (!context.Users.Any())
                        {
                            SeedAdmin(serviceProvider).Wait();
                        }

                        if (!context.Knives.Any())
                        {
                            SeedKnives(context);
                        }
                    }
                }

                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        // Seed
        private async Task SeedAdmin(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var username = this.Configuration.GetValue<string>("AdminConfig:Username");
            var email = this.Configuration.GetValue<string>("AdminConfig:Email");
            var password = this.Configuration.GetValue<string>("AdminConfig:Password");

            var user = new ApplicationUser
            {
                Email = email,
                UserName = username,
                EmailConfirmed = true,
            };

            await userManager.CreateAsync(user, password);
        }

        private void SeedKnives(ApplicationDbContext context)
        {
            var knifes = new Knive[8]
            {
                new Knive
                {
                    KniveName = "The Rush",
                    HandleType = "plastic",
                    EdgeLength = 6,
                    BladeType = "Steel",
                    Type = "Tanto blade",
                    Price = 40,
                },
                new Knive
                {
                    KniveName="Stealthy",
                    HandleType = "Polimer",
                    EdgeLength = 5.5,
                    BladeType = "Steel",
                    Type = "Straight blade",
                    Price = 45,
                },
                new Knive
                {
                    KniveName="The Tough",
                     HandleType = "Mixed alloy",
                    EdgeLength = 7,
                    BladeType = "Titanium",
                    Type = "Jagged blade",
                    Price = 70,
                },
                new Knive
                {
                    KniveName = "Curvy",
                    HandleType = "Elephant tusk",
                    EdgeLength = 6.5,
                    BladeType = "Stainless steel",
                    Type = "Curved blade",
                    Price = 85,
                },
                 new Knive
                {
                     KniveName= "Straight cut",
                    HandleType = "plastic",
                    EdgeLength = 12,
                    BladeType = "Steel",
                    Type = "Tanto blade",
                    Price = 90,
                },
                new Knive
                {
                    KniveName="Long and sharp",
                    HandleType = "Polimer + Plastic",
                    EdgeLength = 10.5,
                    BladeType = "Steel",
                    Type = "Straight blade",
                    Price = 90,
                },
                new Knive
                {
                    KniveName = "Pointy",
                    HandleType = "Mixed alloy",
                    EdgeLength = 7.5,
                    BladeType = "Titanium",
                    Type = "Jagged blade",
                    Price = 70,
                },
                new Knive
                {
                    KniveName = "Blood rain",
                    HandleType = "Elephant tusk",
                    EdgeLength = 6.5,
                    BladeType = "Stainless steel",
                    Type = "Curved blade",
                    Price = 130,
                }
            };

            context.Knives.AddRangeAsync(knifes).Wait();
            context.SaveChangesAsync().Wait();
        }
    }
}
