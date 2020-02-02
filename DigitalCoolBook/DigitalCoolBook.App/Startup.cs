using System.Collections.Generic;
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
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Linq;
using AutoMapper;

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

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
            });
            services.AddAutoMapper();
            services.AddMvc(mvcOptions =>
            {
                mvcOptions.EnableEndpointRouting = false;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

                using (var serviceScope = app.ApplicationServices.CreateScope())
                {
                    using (var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        context.Database.EnsureCreated();
                        this.SeedDbAsync(context, serviceProvider).Wait();
                    }
                }
                this.CreateRoles(serviceProvider).Wait();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            };

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private async Task SeedDbAsync(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            if (!context.Grades.Any())
            {
                context.Grades.AddRange(this.AddParalelos());
                context.SaveChanges();
            }

            if (!context.Subjects.Any())
            {
                context.Subjects.AddRange(this.AddSubjects());
                context.SaveChanges();
            }

            if (!context.Teachers.Any())
            {
                var teachers = this.AddTeachers(userManager);

                foreach (var teacher in teachers)
                {
                    var result = await userManager.CreateAsync(teacher, teacher.PasswordHash);
                    await userManager.AddToRoleAsync(teacher, "Teacher");
                }

                await context.SaveChangesAsync();
            }

            //if (!context.GradeParalelos.Any())
            //{
            //    context.GradeParalelos.AddRange(this.AddGradeParalelo());
            //    context.SaveChanges();
            //}

            if (!context.Students.Any())
            {
                var students = this.AddStudents(userManager);

                foreach (var student in students)
                {
                    var result = await userManager.CreateAsync(student, student.PasswordHash);
                    await userManager.AddToRoleAsync(student, "Student");
                }
                await context.SaveChangesAsync();
            }
        }

        private GradeParalelo[] AddGradeParalelo()
        {
            GradeParalelo[] gradeParalelos = new GradeParalelo[4]
            {
                new GradeParalelo
                {
                   GradeParaleloId = Guid.NewGuid().ToString(),
                    IdGrade = "047c959b-c2d4-4d49-acb9-c096660fc1d7",
                    IdTeacher = "0b0ebf61-11ff-487a-ae66-a257787d77d1"
                },
                new GradeParalelo
                {
                    GradeParaleloId = Guid.NewGuid().ToString(),
                     IdGrade ="0a5a0880-ae45-4c1b-97ee-bab17775bce1",
                    IdTeacher = "1a928ae1-95ad-479a-9f79-32981787c45b"
                },
                new GradeParalelo
                {
                    GradeParaleloId = Guid.NewGuid().ToString(),
                     IdGrade = "0f4484a8-c74e-405d-b6cc-3c5bc581929f",
                    IdTeacher = "5757242a-6635-4186-a9ac-05c3b165359e"
                },
                new GradeParalelo
                {
                    GradeParaleloId = Guid.NewGuid().ToString(),
                     IdGrade = "29bae06b-3e83-4e2f-8a80-d87a3be14e96",
                    IdTeacher = "1a928ae1-95ad-479a-9f79-32981787c45b"
                }
            };
            return gradeParalelos;
        }

        private Student[] AddStudents(UserManager<IdentityUser> userManager)
        {
            Student[] students = new Student[3]
             {
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 099760043,
                    Name = "Ceco Ivanov",
                    PasswordHash = "Cec155*",
                    PlaceOfBirth = "Botevgrad",
                    Sex = "Male",
                    Telephone = 8765,
                    Address = "Stoicho Popov 12",
                    FatherName = "Atanas",
                    FatherMobileNumber = 09876554,
                    //IdGradeParalelo = "66c50edf-b7fb-44a4-90b2-c4e7d943a522",
                    MotherName = "Stoqnka",
                    MotherMobileNumber = 099999933,
                    Email = "ceco@ceco.com",
                    UserName = "ceco@ceco.com"
                },
                new Student
                {
                     Id = Guid.NewGuid().ToString(),
                    MobilePhone = 094098321,
                    Name = "Ivailo Dimitrov",
                    PasswordHash = "Ivo155*",
                    PlaceOfBirth = "Pirdop",
                    Sex = "Male",
                    Telephone = 3234,
                     Address = "Anev Popov 12",
                    FatherName = "Todor",
                    FatherMobileNumber = 098434554,
                    //IdGradeParalelo = "7aabcd2f-2db3-443a-a9cc-b11b105d9c1f",
                    MotherName = "Donka",
                    MotherMobileNumber = 099009933,
                    Email = "ivailo@ivailo.com",
                    UserName = "ivailo@ivailo.com"
                },
                new Student
                {
                     Id = Guid.NewGuid().ToString(),
                    MobilePhone = 0978755442,
                    Name = "Mariq Ignatova",
                    PasswordHash = "Mar155*",
                    PlaceOfBirth = "Kurtovo Konare",
                    Sex = "Female",
                    Telephone = 3300,
                     Address = "Stoicho Simeonov 17",
                    FatherName = "John Tailer",
                    FatherMobileNumber = 09870054,
                   // IdGradeParalelo = "d019c0ab-b482-422b-8209-fb810aced29d",
                    MotherName = "Penka",
                    MotherMobileNumber = 0997699933,
                      Email = "mima@mima.com",
                      UserName = "mima@mima.com"
                }
             };
            return students;
        }

        private Teacher[] AddTeachers(UserManager<IdentityUser> userManager)
        {
            Teacher[] teachers = new Teacher[3]
            {
                new Teacher
                {
                    Id = Guid.NewGuid().ToString(),
                    DateOfBirth = DateTime.Parse("05/2/1990"),
                    Email = "tot@tot.com",
                    MobilePhone = 0997655443,
                    Name = "Pavlina",
                    PasswordHash = "Tot155*",
                    PlaceOfBirth = "Plovdiv",
                    Sex = "Female",
                    Telephone = 3344,
                    UserName = "tot@tot.com"
                },

                new Teacher
                {
                    Id = Guid.NewGuid().ToString(),
                     DateOfBirth = DateTime.Parse("05/2/1987"),
                    Email = "stam@stam.com",
                    MobilePhone = 099456373,
                    Name = "Stamat Ionchev",
                    PasswordHash = "Stam155*",
                    PlaceOfBirth = "Plovdiv",
                    Sex = "Male",
                    Telephone = 3264,
                    UserName = "stam@stam.com"
                },
                new Teacher
                {
                    Id = Guid.NewGuid().ToString(),
                     DateOfBirth = DateTime.Parse("05/2/1980"),
                    Email = "pesh@pesh.com",
                    MobilePhone = 0997655442,
                    Name = "Pesho Geshev",
                    PasswordHash = "Pesh155*",
                    PlaceOfBirth = "Sofia",
                    Sex = "Male",
                    Telephone = 3346,
                    UserName = "pesh@pesh.com"
                }
            };
            return teachers;
        }

        private List<Grade> AddParalelos()
        {
            var paraleloArray = new string[8] { "а", "б", "в", "г", "д", "е", "ж", "з" };
            var grades = new List<Grade>();

            foreach (var paralelo in paraleloArray)
            {
                for (int j = 1; j <= 12; j++)
                {
                    var grade = new Grade
                    {
                        GradeId = Guid.NewGuid().ToString(),
                        Name = j.ToString() + paralelo
                    };
                    grades.Add(grade);
                }
            }
            return grades;
        }

        private List<Subject> AddSubjects()
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
                    SubjectId = Guid.NewGuid().ToString(),
                    Name = subject
                };

                subjectsList.Add(subjectForContext);
            }
            return subjectsList;
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles 
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            bool isUserAddedInRole = await roleManager.RoleExistsAsync("Admin");

            if (!isUserAddedInRole)
            {
                // first we create Admin rool    
                var role = new IdentityRole();
                role.Name = "Admin";
                await roleManager.CreateAsync(role);

            }

            if (!context.Users.Any(u=>u.Email == "admin@admin.com"))
            {
                var user = new IdentityUser();
                user.UserName = Configuration.GetValue<string>("AdminConfig:Username");
                user.Email = Configuration.GetValue<string>("AdminConfig:Email");
                var userPassword = Configuration.GetValue<string>("AdminConfig:Password");

                IdentityResult addingPasswordToUser = await userManager.CreateAsync(user, userPassword);

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

