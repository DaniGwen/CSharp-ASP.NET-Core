using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;
using KniveGallery.Data;
using KniveGallery.Data.Models;

namespace KnifeGallery
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
            services.AddDbContext<GalleryDbContext>(options =>
                options.UseSqlServer(this.Configuration.GetSection("DefaultConnection").Value));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<GalleryDbContext>();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
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

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Add CORS to enable Angular to communicate with the API server
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                using (var serviceScope = app.ApplicationServices.CreateScope())
                {
                    using (var context = serviceScope.ServiceProvider.GetRequiredService<GalleryDbContext>())
                    {
                        context.Database.EnsureCreated();

                        if (!context.Users.Any())
                        {
                            SeedAdmin(context, serviceProvider);
                        }

                        if (!context.Knives.Any())
                        {
                            SeedKnives(context);
                        }
                    }
                }

                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();

            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // Seed
        private void SeedAdmin(GalleryDbContext context, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var username = this.Configuration.GetValue<string>("AdminConfig:Username");
            var email = this.Configuration.GetValue<string>("AdminConfig:Email");
            var password = this.Configuration.GetValue<string>("AdminConfig:Password");

            var user = new IdentityUser
            {
                Email = email,
                UserName = username,
            };

            userManager.CreateAsync(user, password).Wait();
        }
        private void SeedKnives(GalleryDbContext context)
        {
            var knifes = new Knive[4]
            {
                new Knive
                {
                    HandleType = "plastic",
                    EdgeLength = 6,
                    BladeType = "Steel",
                    Type = "Tanto blade",
                    Price = 40,
                },
                new Knive
                {
                    HandleType = "Polimer",
                    EdgeLength = 5.5,
                    BladeType = "Steel",
                    Type = "Straight blade",
                    Price = 45,
                },
                new Knive
                {
                     HandleType = "Mixed alloy",
                    EdgeLength = 7,
                    BladeType = "Titanium",
                    Type = "Jagged blade",
                    Price = 70,
                },
                new Knive
                {
                    HandleType = "Elephant tusk",
                    EdgeLength = 6.5,
                    BladeType = "Stainless steel",
                    Type = "Curved blade",
                    Price = 85,
                }
            };

            context.Knives.AddRangeAsync(knifes).Wait();
            context.SaveChangesAsync().Wait();
        }
    }
}
