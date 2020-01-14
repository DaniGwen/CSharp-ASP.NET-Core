using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using DigitalCoolBook.App.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DigitalCoolBook.Models;
using System.Threading.Tasks;
using System;
using DigitalCoolBook.App.Configuration;

namespace DigitalCoolBook.App
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

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = false;
            });

            services.AddMvc(mvcOptions =>
            {
                mvcOptions.EnableEndpointRouting = false;
            });
            services.AddOptions();
            services.Configure<AdminConfig>(Configuration.GetSection("AdminConfig"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            this.CreateRoles(serviceProvider).Wait();

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    context.Database.EnsureCreated();

                    if (!context.Grades.Any())
                    {
                        var paraleloArray = new string[8] { "а", "б", "в", "г", "д", "е", "ж", "з" };
                        var grades = new List<Grade>();

                        foreach (var paralelo in paraleloArray)
                        {
                            for (int j = 1; j <= 12; j++)
                            {
                                var grade = new Grade
                                {
                                    Name = j.ToString() + paralelo
                                };
                                grades.Add(grade);
                            }
                        }
                        context.Grades.AddRange(grades);
                        context.SaveChanges();
                    }

                    if (!context.Subjects.Any())
                    {
                        var subjects = new List<string>()
                        {
                            "Математика", "Български", "Литература", "География", "История", "Графичен дизайн"
                        };

                        var subjectsList = new List<Subject>();

                        foreach (var subject in subjects)
                        {
                            var subjectForContext = new Subject
                            {
                                Name = subject
                            };

                            subjectsList.Add(subjectForContext);
                        }
                        context.Subjects.AddRange(subjectsList);
                        context.SaveChanges();
                    }
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            };

            app.UseCookiePolicy();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMvcWithDefaultRoute();

        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles 
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            bool isUserAddedInRole = await roleManager.RoleExistsAsync("Admin");
            if (!isUserAddedInRole)
            {
                // first we create Admin rool    
                var role = new IdentityRole();
                role.Name = "Admin";
                await roleManager.CreateAsync(role);

                //Here we create a Admin super user who will maintain the website                   

                var user = new IdentityUser();
                user.UserName = Configuration.GetValue<string>("AdminConfig:Username");
                user.Email = Configuration.GetValue<string>("AdminConfig:Email");

                var userPassword = Configuration.GetValue<string>("AdminConfig:Password");

                IdentityResult addingPasswordToUser = await userManager.CreateAsync(user, userPassword.ToString());

                //Add default User to Role Admin    
                if (addingPasswordToUser.Succeeded)
                {
                    var result1 = await userManager.AddToRoleAsync(user, "Admin");
                }
            }

            // creating Creating Student role     
            isUserAddedInRole = await roleManager.RoleExistsAsync("Student");
            if (!isUserAddedInRole)
            {
                var role = new IdentityRole();
                role.Name = "Student";
                await roleManager.CreateAsync(role);
            }

            // creating Creating Teacher role     
            isUserAddedInRole = await roleManager.RoleExistsAsync("Teacher");
            if (!isUserAddedInRole)
            {
                var role = new IdentityRole();
                role.Name = "Teacher";
                await roleManager.CreateAsync(role);
            }
        }
    }
}

