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
                   Title = "Живот и творчество",
                   Lessons = new List<Lesson>(this.AddLessons("1")),
                   SubjectId = context.Subjects.FirstOrDefault(s => s.Name == "Изобразително изкуство").SubjectId,
               },
               new Category()
               {
                   Id = 2.ToString(),
                   Title = "Творци на модерното изкуство",
                   Lessons = new List<Lesson>(this.AddLessons("2")),
                   SubjectId = context.Subjects.FirstOrDefault(s => s.Name == "Изобразително изкуство").SubjectId,
               },
               new Category()
               {
                   Id = 3.ToString(),
                   Title = "Световни музей и галерии",
                   Lessons = new List<Lesson>(this.AddLessons("3")),
                   SubjectId = context.Subjects.FirstOrDefault(s => s.Name == "Изобразително изкуство").SubjectId,
               },
               new Category()
               {
                   Id = 4.ToString(),
                   Title = "Протестно Изкуство",
                   Lessons = new List<Lesson>(this.AddLessons("4")),
                   SubjectId = context.Subjects.FirstOrDefault(s => s.Name == "Изобразително изкуство").SubjectId,
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
                        Title = "Живот и творчество на художника",
                        Content = "„Днес може би малцина си дават точна сметка за онова, което изгубваме. Може би мнозина не знаят името на тоя извънредно даровит млад човек, отгледан в оскъдица от баща овчар. Но ще минат години и всички ще почувстваме цената на златото, което той носеше в душата си. И с щедростта на приказен цар разсипваше в своите декоративни видения” " +
                          "Сирак Скитник, в. „Слово”, 1927 г., бр. 1388",
                        CategoryId = id,
                    },
                    new Lesson
                    {
                        LessonId = 20.ToString(),
                        Title = "Творчески процес",
                        Content = "Творчество e умствен и обществен процес на човешката дейност, при който се създават качествено нови материални и духовни ценности. Свързан е с генерирането на нови идеи и понятия, или пък нови връзки между вече съществуващи такива. Съществената разлика с производството е оригиналността на новия продукт. Творчеството е също така неразривно свързано със свободата.",
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
                        Title = "Творци на модерното изкуство",
                        Content = "Първите крачки в модерното изкуство са направени от импресионистите и под влиянието на Едуард Мане през 1880-те години. Освен че отричат нормите на френската академия на изкуствата – нещо което преди тях са сторили и художниците на реализма – те добавят и съвсем нови елементи в рисуването. Примери за това са използването на чисти бои и рисуването чрез нанасяне на щрихи. Това отричане на класическото изкуство отваря пътя на художници като Сезан, Гоген и Ван Гог и към стилове като кубизма.",
                        CategoryId = id,
                    },
                    new Lesson
                    {
                        LessonId = 40.ToString(),
                        Title = "Творци на съвременното изкуство",
                        Content = "Съвременното изкуство е съвременна живопис създадена в края на 20 век или в 21 век. Творчеството на съвременните живописци е повлияно от средата на глобален, културно разнообразен и технологично развиващ се свят. Съвременната живопис е динамична комбинация от материали, методи, идеи и субекти, които излизат изъвн традиционните граници на мисълта и излизат извън границите на човешкото въображение. Тази живопис е разнообразна и дигитална като цяло се отличава със самата липса на единен, организиращ принцип, идеология или „идеологизъм“. Съвременното изкуство е част от културен диалог, който засяга по-големи общностни рамки като лична и културна идентичност, семейство, общност и националност в света на чо",
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
                        Title = "Световни Музей",
                        Content = "1. Националния дворец музей, Тайпе, Тайван " +
                        "Този величествен музей се намира в Тайпе, Тайван, Китай. Tук ще намерите изложени над 693 507 експоната, изобразяващи древната история на Китай за последните 8000 години. На това място може да получите пълна представа за историята и културата на Китай." +
                        "2. Музея Прадо, Мадрид, Испания" +
                        "Ако се интересувате от историята и културата на Испания, трябва непременно да посетите музея Прадо. Тук ще имате възможността да разгледате много богата колекция от различни експонати от цяла Европа. Музеят разполага с около 950 склуптори, 64000 рисунки, 2400 щампи, 800 декоратишни изкуства, 900 монети и 800 медала...",
                        CategoryId = id,
                    },
                    new Lesson
                    {
                        LessonId = 60.ToString(),
                        Title = "Галерии",
                        Content = "От 1998 г. Берлинската картинна галерия се помества в специално построена за нея сграда в Културфорума. В проекта на новата музейна сграда, изпълнена от архитектите Хайнц Хилмер, Христоф Затлер и Томас Албрехт, е включена вилата на издателя Паул Парей. Северната фасада на правоъгълното здание е леко изтеглено напред. Цокълът на фасадата навява на италианския Ренесанс и пруския класицизъм.",
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
                        Title = "Художествени форми на протест",
                        Content = "Още преди две години обществените места в Истанбул бяха предвидени за творческите изяви на общо 88 творци от различни държави. Художествените инсталации и творби трябваше да се разположат из парка Гези, на площад Таксим, из застрашения от разрушаване квартал - т.е. все на места, за които дискутира цяла Турция. Истанбулската градска управа обаче не даде разрешение за ползването на обществените места като изложбени зали",
                        CategoryId = id,
                    },
                    new Lesson
                    {
                        LessonId = 80.ToString(),
                        Title = "Форми на протестното изкуство",
                        Content = "Всяко разминаване с комунистическата идеология, включително в областта на изкуството, се преследва, отстранява, а в част от случаите се унищожава физически. Модерното изкуство се заклеймява като вражеско и човеконенавистно, и се обявява за продукт на психично болни и психопати. Забранено е внасянето на книги, албуми и каталози, както и организиране на изложби с потенциално модерен характер.",
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
                   IdGrade = context.Grades.First(grade => grade.Name == "10а").GradeId,
                   IdTeacher = context.Teachers.First(teacher => teacher.Email == "tot@tot.com").Id,
                },

                new GradeParalelo
                {
                    GradeParaleloId = "2",
                    IdGrade = context.Grades.First(grade => grade.Name == "10е").GradeId,
                    IdTeacher = context.Teachers.First(teacher => teacher.Email == "stam@stam.com").Id,
                },

                new GradeParalelo
                {
                    GradeParaleloId = "3",
                    IdGrade = context.Grades.First(grade => grade.Name == "10б").GradeId,
                    IdTeacher = context.Teachers.First(teacher => teacher.Email == "pesh@pesh.com").Id,
                },

                new GradeParalelo
                {
                    GradeParaleloId = "4",
                    IdGrade = context.Grades.First(grade => grade.Name == "12а").GradeId,
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
                    GradeId = context.Grades.First(g => g.Name == "10е").GradeId,
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
                    GradeId = context.Grades.First(g => g.Name == "10а").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 0978755442,
                    Name = "Mariq Ignatova",
                    PasswordHash = "Mar155*",
                    PlaceOfBirth = "Мелник",
                    Sex = "Female",
                    Telephone = 3300,
                    Address = "Stoicho Simeonov 17",
                    FatherName = "John Tailer",
                    FatherMobileNumber = 09870054,
                    MotherName = "Penka",
                    MotherMobileNumber = 0997699933,
                    Email = "mima@mima.com",
                    UserName = "mima@mima.com",
                    GradeId = context.Grades.First(g => g.Name == "10а").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 0978757342,
                    Name = "Атанас Сакъзов",
                    PasswordHash = "Atanas155*",
                    PlaceOfBirth = "Мелник",
                    Sex = "Мале",
                    Telephone = 3400,
                    Address = "Радко Димитриев 19",
                    FatherName = "Свилен Сакъзов",
                    FatherMobileNumber = 098722054,
                    MotherName = "Генка Шикерова",
                    MotherMobileNumber = 0987699033,
                    Email = "atanas@atanas.com",
                    UserName = "atanas@atanas.com",
                    GradeId = context.Grades.First(g => g.Name == "10е").GradeId,
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
                    GradeId = context.Grades.First(g => g.Name == "10б").GradeId,
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
                    GradeId = context.Grades.First(g => g.Name == "10б").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 099760043,
                    Name = "Ивелин Тошев",
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
                    GradeId = context.Grades.First(g => g.Name == "10б").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 099760043,
                    Name = "Маньо Минчев",
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
                    GradeId = context.Grades.First(g => g.Name == "10а").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 099760043,
                    Name = "Щефан Сандерс",
                    PasswordHash = "Shtef155*",
                    PlaceOfBirth = "Костинброд",
                    Sex = "Male",
                    Telephone = 876521,
                    Address = "Петър Стоев 10",
                    FatherName = "Велизар",
                    FatherMobileNumber = 09876554,
                    MotherName = "Петра",
                    MotherMobileNumber = 099999933,
                    Email = "shtef@shtef.com",
                    UserName = "shtef@shtef.com",
                    GradeId = context.Grades.First(g => g.Name == "10а").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 099760043,
                    Name = "Богомил Милев",
                    PasswordHash = "Bog155*",
                    PlaceOfBirth = "Елин Пелин",
                    Sex = "Male",
                    Telephone = 098765,
                    Address = "Княз Момчил 2",
                    FatherName = "Иван",
                    FatherMobileNumber = 09876554,
                    MotherName = "Стела",
                    MotherMobileNumber = 099999933,
                    Email = "bog@bog.com",
                    UserName = "bog@bog.com",
                    GradeId = context.Grades.First(g => g.Name == "10а").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 099760043,
                    Name = "Десислава Атанасова",
                    PasswordHash = "Des155*",
                    PlaceOfBirth = "Благоевград",
                    Sex = "Female",
                    Telephone = 88899,
                    Address = "Богомил 2",
                    FatherName = "Светослав",
                    FatherMobileNumber = 09876554,
                    MotherName = "Биляна",
                    MotherMobileNumber = 099999933,
                    Email = "des@des.com",
                    UserName = "des@des.com",
                    GradeId = context.Grades.First(g => g.Name == "10а").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 099760043,
                    Name = "Радослав Първанов",
                    PasswordHash = "Rad155*",
                    PlaceOfBirth = "Перник",
                    Sex = "Male",
                    Telephone = 87652,
                    Address = "Stoicho Popov 12",
                    FatherName = "Atanas",
                    FatherMobileNumber = 09876554,
                    MotherName = "Stoqnka",
                    MotherMobileNumber = 099999933,
                    Email = "rad@rad.com",
                    UserName = "rad@rad.com",
                    GradeId = context.Grades.First(g => g.Name == "10а").GradeId,
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
            var paraleloArray = new string[8] { "а", "б", "в", "г", "д", "е", "ж", "з" };
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
                            "Математика", "Български", "Литература",
                            "География", "История", "Компютърна графика",
                            "Изобразително изкуство",
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