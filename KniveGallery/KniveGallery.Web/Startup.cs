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
                        //context.Database.EnsureDeleted();
                        //context.Database.EnsureCreated();

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
            var knifes = new Knive[8]
            {
                new Knive
                {
                    KniveId= 10,
                     HandleDescription = "Дръжката е комбинация от стаб. Мамутов бивен оцветен със син пигмент на Alexander Boychenko,гард и пета - хромникел.Канията е от работилницата на ‘Вулкан’",
                    EdgeLength = 128,
                    EdgeWidth = 5,
                    EdgeThickness = 5,
                    Price = 100,
                    KniveClass = KniveClass.HighClass,
                    ImagePath = "high1.jpg",
                    EdgeMade = "Marlin man  е с острие от пет модулен мозаечен дамаск",
                    TotalLength= 252,
                    Quantity = 3
                },
                new Knive
                {
                     KniveId= 20,
                     HandleDescription = "Чирените са от стаб.Български клен,оцветен с черен пигмент",
                    EdgeLength = 6.5,
                    EdgeWidth = 4,
                    EdgeThickness = 4.5,
                    Price = 80,
                    KniveClass = KniveClass.HighClass,
                    ImagePath = "high2.jpg",
                     EdgeMade = "немска неръж. стомана М68",
                    TotalLength= 245,
                     Quantity = 3
                },
                new Knive
                {
                     KniveId= 30,
                     HandleDescription = "Дръжката е от Еревански орех на Валентина Вака",
                    EdgeLength = 145,
                    EdgeWidth = 30,
                    EdgeThickness = 4.5,
                    Price = 110,
                    KniveClass = KniveClass.HighClass,
                    ImagePath = "high3.jpg",
                     EdgeMade = "Острието на ножът направих от масат на марката Zwilling от началото на миналия век.",
                    TotalLength= 275,
                     Quantity = 3
                },
                new Knive
                {
                     KniveId= 40,
                     HandleDescription = "Български кленов наплив.",
                    EdgeLength = 220,
                    EdgeWidth = 4,
                    EdgeThickness =3.2,
                    Price = 75,
                    KniveClass = KniveClass.KitchenClass,
                    ImagePath = "kitchen1.jpg",
                     EdgeMade = "френска нерж.стомана ‘Т5’",
                    TotalLength= 320,
                     Quantity = 2
                },
                 new Knive
                {
                      KniveId= 50,
                     HandleDescription = "чирени от стаб.клен",
                    EdgeLength = 6.5,
                    EdgeWidth = 4,
                    EdgeThickness =3.2,
                    Price = 90,
                    KniveClass = KniveClass.KitchenClass,
                    ImagePath = "kitchen2.jpg",
                    EdgeMade = "френска стомана ‘Т5’",
                    TotalLength= 320,
                     Quantity = 4
        },
                new Knive
                {
                     KniveId= 60,
                    HandleDescription = "чирени от мамутов бивен на Скиф Денис ",
                    EdgeLength = 6.5,
                    EdgeWidth = 4,
                    EdgeThickness =5.2,
                    TotalLength=250,
                    Price = 90,
                    KniveClass = KniveClass.MiddleClass,
                    ImagePath = "middle1.jpg",
                    EdgeMade = "Стоманата е ‘Sweden AK5’",
                     Quantity = 3
                },
                new Knive
                {
                     KniveId= 70,
                    HandleDescription = "чирени от мамутов бивен на Скиф Денис ",
                    EdgeLength = 6.5,
                    EdgeWidth = 4,
                    EdgeThickness =3,
                    Price = 70,
                    KniveClass = KniveClass.KitchenClass,
                    ImagePath = "kitchen2.jpg",
                    EdgeMade = "Carbon",
                     Quantity = 6
                },
                new Knive
                {
                     KniveId= 80,
                    HandleDescription = "Дръжката е от стаб.американски клен на Alexander Boychenko",
                    EdgeLength = 140,
                    EdgeWidth = 4,
                    EdgeThickness =5.2,
                    TotalLength= 270,
                    EdgeMade = "острие от немска неръж.стомана с номер “М68”",
                    Price = 130,
                    KniveClass = KniveClass.MiddleClass,
                    ImagePath = "middle2.jpg",
                     Quantity = 3
                }
            };

            context.Knives.AddRangeAsync(knifes).Wait();
            context.SaveChangesAsync().Wait();
        }
    }
}
