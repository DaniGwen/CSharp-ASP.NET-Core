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
using Microsoft.AspNetCore.Http.Features;
using KniveGallery.Web.Models.Enums;
using KniveGallery.Web.Services.EmailService;

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

            services.AddAuthentication().AddIdentityServerJwt();

            services.AddControllersWithViews();

            services.AddRazorPages();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecific", policyBuilder =>
                policyBuilder
                .WithOrigins("https://localhost:44379",
                "http://localhost:61728")
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

            services.AddTransient<EmailService>();

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
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();

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

            app.UseCors("AllowSpecific");

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
            var phoneNumber = this.Configuration.GetValue<string>("AdminConfig:PhoneNumber");
            var role = this.Configuration.GetValue<string>("AdminConfig:Role");

            var user = new ApplicationUser
            {
                PhoneNumber = phoneNumber,
                Email = email,
                UserName = username,
                EmailConfirmed = true,
                Role = role,
            };

            await userManager.CreateAsync(user, password);
        }

        private void SeedKnives(ApplicationDbContext context)
        {
            var knifes = new Knive[9]
            {
                new Knive
                {
                     HandleDescription = "Elephant tusk",
                    EdgeLength = 6.5,
                    EdgeWidth = 4,
                    EdgeThickness =3,
                    Price = 40,
                    KniveClass = KniveClass.HighClass,
                    ImagePath = "high1.jpg",
                     EdgeMade = "Steel",
                    TotalLength= 9,
                },
                new Knive
                {
                     HandleDescription = "Mixed Polimer",
                    EdgeLength = 6.5,
                    EdgeWidth = 4,
                    EdgeThickness =3,
                    Price = 45,
                    KniveClass = KniveClass.MiddleClass,
                    ImagePath = "middle1.jpg",
                     EdgeMade = "Stone",
                    TotalLength= 18,
                },
                new Knive
                {
                     HandleDescription = "Mixed type",
                    EdgeLength = 6.5,
                    EdgeWidth = 4,
                    EdgeThickness =3,
                    Price = 70,
                    KniveClass = KniveClass.HighClass,
                    ImagePath = "high2.jpg",
                     EdgeMade = "Carbon-Ceramic",
                    TotalLength= 15,
                },
                new Knive
                {
                     HandleDescription = "Elephant tusk",
                    EdgeLength = 6.5,
                    EdgeWidth = 4,
                    EdgeThickness =3,
                    Price = 85,
                    KniveClass = KniveClass.KitchenClass,
                    ImagePath = "kitchen1.jpg",
                     EdgeMade = "Ceramic",
                    TotalLength= 10,
                },
                 new Knive
                {
                     HandleDescription = "Plastic",
                    EdgeLength = 6.5,
                    EdgeWidth = 4,
                    EdgeThickness =3,
                    Price = 90,
                    KniveClass = KniveClass.MiddleClass,
                    ImagePath = "middle2.jpg",
                    EdgeMade = "Carbon-Ceramic",
                    TotalLength= 10,
        },
                new Knive
                {
                    HandleDescription = "Polymer and Plastic",
                    EdgeLength = 6.5,
                    EdgeWidth = 4,
                    EdgeThickness =3,
                    Price = 90,
                    KniveClass = KniveClass.HighClass,
                    ImagePath = "high3.jpg",
                    EdgeMade = "Aluminum"
                },
                new Knive
                {
                    HandleDescription = "Mixed alloy",
                    EdgeLength = 6.5,
                    EdgeWidth = 4,
                    EdgeThickness =3,
                    Price = 70,
                    KniveClass = KniveClass.KitchenClass,
                    ImagePath = "kitchen2.jpg",
                    EdgeMade = "Carbon"
                },
                new Knive
                {
                    HandleDescription = "Elephant tusk",
                    EdgeLength = 6.5,
                    EdgeWidth = 4,
                    EdgeThickness =3,
                    EdgeMade = "Stainless steel",
                    Price = 130,
                    KniveClass = KniveClass.KitchenClass,
                    ImagePath = "kitchen3.jpg",
                },
                   new Knive
                {
                    HandleDescription = "plastic ",
                    EdgeLength = 12,
                    EdgeThickness = 2,
                    EdgeWidth = 8,
                    EdgeMade = "Steel",
                    Price = 65,
                    KniveClass = KniveClass.MiddleClass,
                    ImagePath = "middle3.jpg",
                }
            };

            context.Knives.AddRangeAsync(knifes).Wait();
            context.SaveChangesAsync().Wait();
        }
    }
}
