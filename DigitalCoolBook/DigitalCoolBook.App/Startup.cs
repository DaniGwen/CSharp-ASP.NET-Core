namespace DigitalCoolBook.App
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Encodings.Web;
    using System.Text.Unicode;
    using System.Threading.Tasks;
    using AutoMapper;
    using DigitalCoolBook.App.Data;
    using DigitalCoolBook.App.Services;
    using DigitalCoolBook.Models;
    using DigitalCoolBook.Service;
    using DigitalCoolBook.Services;
    using DigitalCoolBook.Services.Contracts;
    using DigitalCoolBook.Services.Mapping;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.WebEncoders;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IGradeService, GradeService>();
            services.AddTransient<ISubjectService, SubjectService>();
            services.AddTransient<ITestService, TestService>();
            services.AddTransient<IQuestionService, QuestionService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<OrganizationProfile>();
            });
            var mapper = config.CreateMapper();

            services.AddAutoMapper(typeof(Startup));
            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
            });

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
                        this.CreateRoles(serviceProvider).Wait();
                        this.SeedDbAsync(context, serviceProvider).Wait();
                    }
                }
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

            if (!context.Students.Any())
            {
                var students = this.AddStudents(userManager, context);

                foreach (var student in students)
                {
                    var result = await userManager.CreateAsync(student, student.PasswordHash);
                    await userManager.AddToRoleAsync(student, "Student");
                }

                await context.SaveChangesAsync();
            }

            if (!context.GradeParalelos.Any())
            {
                context.GradeParalelos.AddRange(this.AddGradeParalelo(context));
                context.SaveChanges();
            }

            if (!context.Categories.Any())
            {
                List<Category> cathegories = this.AddCategories(context);
                await context.Categories.AddRangeAsync(cathegories);
                await context.SaveChangesAsync();
            }
        }

        private List<Category> AddCategories(ApplicationDbContext context)
        {
            var cathegories = new List<Category>
           {
               new Category()
               {
                   Id = 1.ToString(),
                   Title = "����� � ����������",
                   Lessons = new List<Lesson>(this.AddLessons("1")),
                   SubjectId = context.Subjects.FirstOrDefault(s => s.Name == "������������� ��������").SubjectId,
               },
               new Category()
               {
                   Id = 2.ToString(),
                   Title = "������ �� ��������� ��������",
                   Lessons = new List<Lesson>(this.AddLessons("2")),
                   SubjectId = context.Subjects.FirstOrDefault(s => s.Name == "������������� ��������").SubjectId,
               },
               new Category()
               {
                   Id = 3.ToString(),
                   Title = "�������� ����� � �������",
                   Lessons = new List<Lesson>(this.AddLessons("3")),
                   SubjectId = context.Subjects.FirstOrDefault(s => s.Name == "������������� ��������").SubjectId,
               },
               new Category()
               {
                   Id = 4.ToString(),
                   Title = "��������� ��������",
                   Lessons = new List<Lesson>(this.AddLessons("4")),
                   SubjectId = context.Subjects.FirstOrDefault(s => s.Name == "������������� ��������").SubjectId,
               },
           };

            return cathegories;
        }

        private IEnumerable<Lesson> AddLessons(string id)
        {
            var lessons = new List<Lesson>();

            switch (id)
            {
                case "1":
                    lessons = new List<Lesson>()
                {
                    new Lesson
                    {
                        LessonId = 10.ToString(),
                        Title = "����� � ���������� �� ���������",
                        Content = "����� ���� �� ������� �� ����� ����� ������ �� �����, ����� ���������. ���� �� ������� �� ����� ����� �� ��� ���������� ������� ���� �����, �������� � �������� �� ���� �����. �� �� ����� ������ � ������ �� ����������� ������ �� �������, ����� ��� ������ � ������ ��. � � ��������� �� �������� ��� ���������� � ������ ����������� �������� " +
                          "����� �������, �. ������, 1927 �., ��. 1388",
                        CategoryId = id,
                    },
                    new Lesson
                    {
                        LessonId = 20.ToString(),
                        Title = "��������� ������",
                        Content = "���������� e ������� � ��������� ������ �� ��������� �������, ��� ����� �� �������� ���������� ���� ���������� � ������� ��������. ������� � � ������������ �� ���� ���� � �������, ��� ��� ���� ������ ����� ���� ������������ ������. ������������ ������� � �������������� � �������������� �� ����� �������. ������������ � ���� ���� ���������� �������� ��� ���������.",
                        CategoryId = id,
                    },
                };
                    break;
                case "2":
                    lessons = new List<Lesson>()
                {
                    new Lesson
                    {
                        LessonId = 30.ToString(),
                        Title = "������ �� ��������� ��������",
                        Content = "������� ������ � ��������� �������� �� ��������� �� ��������������� � ��� ��������� �� ������ ���� ���� 1880-�� ������. ����� �� ������� ������� �� ��������� �������� �� ���������� � ���� ����� ����� ��� �� ������� � ����������� �� �������� � �� ������� � ������ ���� �������� � ����������. ������� �� ���� �� ������������ �� ����� ��� � ���������� ���� �������� �� �����. ���� �������� �� ������������ �������� ������ ���� �� ��������� ���� �����, ����� � ��� ��� � ��� ������� ���� �������.",
                        CategoryId = id,
                    },
                    new Lesson
                    {
                        LessonId = 40.ToString(),
                        Title = "������ �� ������������ ��������",
                        Content = "������������ �������� � ���������� ������� ��������� � ���� �� 20 ��� ��� � 21 ���. ������������ �� ������������ ��������� � �������� �� ������� �� ��������, �������� ������������ � ������������ �������� �� ����. ������������ ������� � ��������� ���������� �� ���������, ������, ���� � �������, ����� ������� ����� ������������� ������� �� ������� � ������� ����� ��������� �� ��������� �����������. ���� ������� � ������������ � ��������� ���� ���� �� �������� ��� ������ ����� �� ������, ����������� �������, ��������� ��� ������������. ������������ �������� � ���� �� �������� ������, ����� ������ ��-������ ��������� ����� ���� ����� � �������� �����������, ���������, ������� � ������������ � ����� �� ��",
                        CategoryId = id,
                    },
                };
                    break;
                case "3":
                    lessons = new List<Lesson>()
                {
                    new Lesson
                    {
                        LessonId = 50.ToString(),
                        Title = "�������� �����",
                        Content = "1. ����������� ������ �����, �����, ������ " +
                        "���� ����������� ����� �� ������ � �����, ������, �����. T�� �� �������� �������� ��� 693 507 ���������, ������������ �������� ������� �� ����� �� ���������� 8000 ������. �� ���� ����� ���� �� �������� ����� ��������� �� ��������� � ��������� �� �����." +
                        "2. ����� �����, ������, �������" +
                        "��� �� ������������ �� ��������� � ��������� �� �������, ������ ���������� �� �������� ����� �����. ��� �� ����� ������������ �� ���������� ����� ������ �������� �� �������� ��������� �� ���� ������. ������ ��������� � ����� 950 ���������, 64000 �������, 2400 �����, 800 ����������� ��������, 900 ������ � 800 ������...",
                        CategoryId = id,
                    },
                    new Lesson
                    {
                        LessonId = 60.ToString(),
                        Title = "�������",
                        Content = "�� 1998 �. ����������� �������� ������� �� �������� � ��������� ��������� �� ��� ������ � ������������. � ������� �� ������ ������� ������, ��������� �� ����������� ����� ������, ������� ������ � ����� �������, � �������� ������ �� �������� ���� �����. ��������� ������ �� ������������� ������ � ���� ��������� ������. ������� �� �������� ������ �� ����������� �������� � ������� ����������.",
                        CategoryId = id,
                    },
                };
                    break;
                case "4":
                    lessons = new List<Lesson>()
                {
                    new Lesson
                    {
                        LessonId = 70.ToString(),
                        Title = "������������ ����� �� �������",
                        Content = "��� ����� ��� ������ ������������ ����� � �������� ���� ���������� �� ����������� ����� �� ���� 88 ������ �� �������� �������. �������������� ���������� � ������ �������� �� �� ���������� �� ����� ����, �� ������ ������, �� ����������� �� ����������� ������� - �.�. ��� �� �����, �� ����� ��������� ���� ������. ������������� ������� ������ ����� �� ���� ���������� �� ���������� �� ������������ ����� ���� ��������� ����",
                        CategoryId = id,
                    },
                    new Lesson
                    {
                        LessonId = 80.ToString(),
                        Title = "����� �� ����������� ��������",
                        Content = "����� ����������� � ���������������� ���������, ����������� � �������� �� ����������, �� ���������, ����������, � � ���� �� �������� �� ��������� ���������. ��������� �������� �� ���������� ���� �������� � ����������������, � �� ������� �� ������� �� �������� ����� � ���������. ��������� � ��������� �� �����, ������ � ��������, ����� � ������������ �� ������� � ����������� ������� ��������.",
                        CategoryId = id,
                    },
                };
                    break;
            }

            return lessons;
        }

        private GradeParalelo[] AddGradeParalelo(ApplicationDbContext context)
        {
            GradeParalelo[] gradeParalelos = new GradeParalelo[4]
            {
                new GradeParalelo
                {
                   GradeParaleloId = "1",
                   IdGrade = context.Grades.First(grade => grade.Name == "10�").GradeId,
                   IdTeacher = context.Teachers.First(teacher => teacher.Email == "tot@tot.com").Id,
                },

                new GradeParalelo
                {
                    GradeParaleloId = "2",
                    IdGrade = context.Grades.First(grade => grade.Name == "10�").GradeId,
                    IdTeacher = context.Teachers.First(teacher => teacher.Email == "stam@stam.com").Id,
                },

                new GradeParalelo
                {
                    GradeParaleloId = "3",
                    IdGrade = context.Grades.First(grade => grade.Name == "10�").GradeId,
                    IdTeacher = context.Teachers.First(teacher => teacher.Email == "pesh@pesh.com").Id,
                },

                new GradeParalelo
                {
                    GradeParaleloId = "4",
                    IdGrade = context.Grades.First(grade => grade.Name == "12�").GradeId,
                    IdTeacher = context.Teachers.First(teacher => teacher.Email == "tot@tot.com").Id,
                },
            };
            return gradeParalelos;
        }

        private Student[] AddStudents(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            Student[] students = new Student[12]
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
                    MotherName = "Stoqnka",
                    MotherMobileNumber = 099999933,
                    Email = "ceco@ceco.com",
                    UserName = "ceco@ceco.com",
                    GradeId = context.Grades.First(g => g.Name == "10�").GradeId,
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
                    MotherName = "Donka",
                    MotherMobileNumber = 099009933,
                    Email = "ivailo@ivailo.com",
                    UserName = "ivailo@ivailo.com",
                    GradeId = context.Grades.First(g => g.Name == "10�").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 0978755442,
                    Name = "Mariq Ignatova",
                    PasswordHash = "Mar155*",
                    PlaceOfBirth = "������",
                    Sex = "Female",
                    Telephone = 3300,
                    Address = "Stoicho Simeonov 17",
                    FatherName = "John Tailer",
                    FatherMobileNumber = 09870054,
                    MotherName = "Penka",
                    MotherMobileNumber = 0997699933,
                    Email = "mima@mima.com",
                    UserName = "mima@mima.com",
                    GradeId = context.Grades.First(g => g.Name == "10�").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 0978757342,
                    Name = "������ �������",
                    PasswordHash = "Atanas155*",
                    PlaceOfBirth = "������",
                    Sex = "����",
                    Telephone = 3400,
                    Address = "����� ��������� 19",
                    FatherName = "������ �������",
                    FatherMobileNumber = 098722054,
                    MotherName = "����� ��������",
                    MotherMobileNumber = 0987699033,
                    Email = "atanas@atanas.com",
                    UserName = "atanas@atanas.com",
                    GradeId = context.Grades.First(g => g.Name == "10�").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 099760043,
                    Name = "Stefan Conev",
                    PasswordHash = "Stef155*",
                    PlaceOfBirth = "Montana",
                    Sex = "Male",
                    Telephone = 8765,
                    Address = "Gen. Petko Stoqnov 12",
                    FatherName = "Samuil",
                    FatherMobileNumber = 09876554,
                    MotherName = "Stoqnka",
                    MotherMobileNumber = 099999933,
                    Email = "stef@stef.com",
                    UserName = "stef@stef.com",
                    GradeId = context.Grades.First(g => g.Name == "10�").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 099760043,
                    Name = "Dragan Tconev",
                    PasswordHash = "Drag155*",
                    PlaceOfBirth = "Svilengrad",
                    Sex = "Male",
                    Telephone = 8765,
                    Address = "Stoicho Popov 12",
                    FatherName = "Atanas",
                    FatherMobileNumber = 09876554,
                    MotherName = "Stoqnka",
                    MotherMobileNumber = 099999933,
                    Email = "drag@drag.com",
                    UserName = "drag@drag.com",
                    GradeId = context.Grades.First(g => g.Name == "10�").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 099760043,
                    Name = "������ �����",
                    PasswordHash = "Ivelin155*",
                    PlaceOfBirth = "Samokov",
                    Sex = "Male",
                    Telephone = 8765,
                    Address = "Stoicho 12",
                    FatherName = "Atanas",
                    FatherMobileNumber = 09876554,
                    MotherName = "Stoqnka",
                    MotherMobileNumber = 099999933,
                    Email = "ivelin@ivelin.com",
                    UserName = "ivelin@ivelin.com",
                    GradeId = context.Grades.First(g => g.Name == "10�").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 099760043,
                    Name = "����� ������",
                    PasswordHash = "Man155*",
                    PlaceOfBirth = "Haskovo",
                    Sex = "Male",
                    Telephone = 8765,
                    Address = "Alexander Nevski 9",
                    FatherName = "Atanas",
                    FatherMobileNumber = 09876554,
                    MotherName = "Maria",
                    MotherMobileNumber = 099999933,
                    Email = "man@man.com",
                    UserName = "man@man.com",
                    GradeId = context.Grades.First(g => g.Name == "10�").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 099760043,
                    Name = "����� �������",
                    PasswordHash = "Shtef155*",
                    PlaceOfBirth = "����������",
                    Sex = "Male",
                    Telephone = 876521,
                    Address = "����� ����� 10",
                    FatherName = "�������",
                    FatherMobileNumber = 09876554,
                    MotherName = "�����",
                    MotherMobileNumber = 099999933,
                    Email = "shtef@shtef.com",
                    UserName = "shtef@shtef.com",
                    GradeId = context.Grades.First(g => g.Name == "10�").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 099760043,
                    Name = "������� �����",
                    PasswordHash = "Bog155*",
                    PlaceOfBirth = "���� �����",
                    Sex = "Male",
                    Telephone = 098765,
                    Address = "���� ������ 2",
                    FatherName = "����",
                    FatherMobileNumber = 09876554,
                    MotherName = "�����",
                    MotherMobileNumber = 099999933,
                    Email = "bog@bog.com",
                    UserName = "bog@bog.com",
                    GradeId = context.Grades.First(g => g.Name == "10�").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 099760043,
                    Name = "��������� ���������",
                    PasswordHash = "Des155*",
                    PlaceOfBirth = "�����������",
                    Sex = "Female",
                    Telephone = 88899,
                    Address = "������� 2",
                    FatherName = "���������",
                    FatherMobileNumber = 09876554,
                    MotherName = "������",
                    MotherMobileNumber = 099999933,
                    Email = "des@des.com",
                    UserName = "des@des.com",
                    GradeId = context.Grades.First(g => g.Name == "10�").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 099760043,
                    Name = "�������� ��������",
                    PasswordHash = "Rad155*",
                    PlaceOfBirth = "������",
                    Sex = "Male",
                    Telephone = 87652,
                    Address = "Stoicho Popov 12",
                    FatherName = "Atanas",
                    FatherMobileNumber = 09876554,
                    MotherName = "Stoqnka",
                    MotherMobileNumber = 099999933,
                    Email = "rad@rad.com",
                    UserName = "rad@rad.com",
                    GradeId = context.Grades.First(g => g.Name == "10�").GradeId,
                },
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
                    UserName = "tot@tot.com",
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
                    UserName = "stam@stam.com",
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
                    UserName = "pesh@pesh.com",
                },
            };
            return teachers;
        }

        private List<Grade> AddParalelos()
        {
            var paraleloArray = new string[8] { "�", "�", "�", "�", "�", "�", "�", "�" };
            var grades = new List<Grade>();

            foreach (var paralelo in paraleloArray)
            {
                for (int j = 1; j <= 12; j++)
                {
                    var grade = new Grade
                    {
                        GradeId = Guid.NewGuid().ToString(),
                        Name = j.ToString() + paralelo,
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
                            "����������", "���������", "����������",
                            "���������", "�������", "���������� �������",
                            "������������� ��������",
                        };

            var subjectsList = new List<Subject>();

            foreach (var subject in subjects)
            {
                var subjectForContext = new Subject
                {
                    SubjectId = Guid.NewGuid().ToString(),
                    Name = subject,
                };

                subjectsList.Add(subjectForContext);
            }

            return subjectsList;
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            // initializing custom roles
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            bool isUserAddedInRole = await roleManager.RoleExistsAsync("Admin");

            if (!isUserAddedInRole)
            {
                // first we create Admin role
                var role = new IdentityRole();
                role.Name = "Admin";
                await roleManager.CreateAsync(role);
            }

            if (!context.Users.Any(u => u.Email == "admin@admin.com"))
            {
                var user = new IdentityUser();
                user.UserName = this.Configuration.GetValue<string>("AdminConfig:Username");
                user.Email = this.Configuration.GetValue<string>("AdminConfig:Email");
                var userPassword = this.Configuration.GetValue<string>("AdminConfig:Password");

                IdentityResult addingPasswordToUser = await userManager.CreateAsync(user, userPassword);

                // Add default User to Role Admin
                if (addingPasswordToUser.Succeeded)
                {
                   await userManager.AddToRoleAsync(user, "Admin");
                }
            }


            // Creating Student role
            isUserAddedInRole = await roleManager.RoleExistsAsync("Student");
            if (!isUserAddedInRole)
            {
                var role = new IdentityRole();
                role.Name = "Student";
                await roleManager.CreateAsync(role);
            }

            // Creating Teacher role
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