using System.Net.Http;
using DigitalCoolBook.App.Controllers;
using DigitalCoolBook.App.Mappings;

namespace DigitalCoolBook.App
{
    using System;
    using System.Globalization;
    using AutoMapper;
    using Data;
    using DigitalCoolBook.Services;
    using DigitalCoolBook.Services.Contracts;
    using Hubs;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using AspNetCoreHero.ToastNotification;
    using DigitalCoolBook.Data;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<CookiePolicyOptions>(
               options =>
               {
                   options.CheckConsentNeeded = context => true;
                   options.MinimumSameSitePolicy = SameSiteMode.None;
               });

            services.AddNotyf(conf =>
            {
                conf.IsDismissable = true;
                conf.HasRippleEffect = true;
                conf.Position = NotyfPosition.TopCenter;
                conf.DurationInSeconds = 10;
            });

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IGradeService, GradeService>();
            services.AddTransient<ISubjectService, SubjectService>();
            services.AddTransient<ITestService, TestService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IScoreService, ScoreService>();
            services.AddTransient<ILiveFeedService, LiveFeedService>();
            services.AddTransient<ITodoService, TodoService>();
            services.AddTransient<ITaskService, TaskService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<OrganizationProfile>();
            });

            services.AddAutoMapper(typeof(Startup));

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;
            });

            services.AddControllersWithViews()
                .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true)
                .AddNewtonsoftJson(options =>
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddServerSideBlazor();

            services.AddMvc().WithRazorPagesRoot("/BlazorPages");

            services.AddSignalR(options => {
                options.EnableDetailedErrors = true; // Remove in production
            }).AddMessagePackProtocol();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                using var serviceScope = app.ApplicationServices.CreateScope();
                using var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

               //context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var seeder = new ApplicationDbSeeder(context, serviceProvider, this.Configuration);
                seeder.CreateRoles().Wait();
                seeder.SeedDbAsync().Wait();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("bg"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoint =>
            {
                endpoint.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoint.MapBlazorHub();
                endpoint.MapFallbackToPage("/_Host");

                endpoint.MapHub<TestHub>("/testhub");
                endpoint.MapHub<LiveFeedHub>("/liveFeedHub");
                endpoint.MapHub<NotifierHub>("/notifierhub");
            });
        }
    }
}