using DigitalCoolBook.Data;

namespace DigitalCoolBook.App
{
    using AutoMapper;
    using Data;
    using DigitalCoolBook.Services;
    using DigitalCoolBook.Services.Contracts;
    using DigitalCoolBook.Services.Mapping;
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
    using Service;
    using System;
    using System.Globalization;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<CookiePolicyOptions>(
               options =>
               {
                   options.CheckConsentNeeded = context => true;
                   options.MinimumSameSitePolicy = SameSiteMode.None;
               });

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IGradeService, GradeService>();
            services.AddTransient<ISubjectService, SubjectService>();
            services.AddTransient<ITestService, TestService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IScoreService, ScoreService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<OrganizationProfile>();
            });
            var mapper = config.CreateMapper();

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

            services.AddMvc(mvcOptions =>
            {
                mvcOptions.EnableEndpointRouting = false;
            });

            services.AddSignalR(options =>
            {
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

                var seeder = new ApplicationDBSeeder(context, serviceProvider, this.Configuration);
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
                // Add english-USA support localization
                // new CultureInfo("en-US"),
                new CultureInfo("bg"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("bg"),
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
                endpoint.MapHub<TestHub>("/testhub");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}