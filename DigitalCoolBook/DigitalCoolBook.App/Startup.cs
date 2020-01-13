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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    context.Database.EnsureCreated();

                    if (!context.UserRoles.Any())
                    {
                        
                    }

                    if (!context.Grades.Any())
                    {
                        var paraleloArray = new string[8] { "�", "�", "�", "�", "�", "�", "�", "�" };
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
                            "����������", "���������", "����������", "���������", "�������", "�������� ������"
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
    }
}
