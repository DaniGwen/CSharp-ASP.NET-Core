using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalCoolBook.App.Data;
using DigitalCoolBook.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalCoolBook.Data
{
    public class ApplicationDbSeeder
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public ApplicationDbSeeder(ApplicationDbContext dbContext
            , IServiceProvider serviceProvider
            , IConfiguration configuration)
        {
            _dbContext = dbContext;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        public async Task SeedDbAsync()
        {
            var userManager = _serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            if (!_dbContext.Grades.Any())
            {
                await _dbContext.Grades.AddRangeAsync(this.AddParalelos());
                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.Subjects.Any())
            {
                await _dbContext.Subjects.AddRangeAsync(this.AddSubjects());
                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.Teachers.Any())
            {
                var teachers = this.AddTeachers(userManager);

                foreach (var teacher in teachers)
                {
                    var result = await userManager.CreateAsync(teacher, teacher.PasswordHash);
                    await userManager.AddToRoleAsync(teacher, "Teacher");
                }

                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.Students.Any())
            {
                var students = this.CreateStudents(_dbContext);

                foreach (var student in students)
                {
                    var result = await userManager.CreateAsync(student, student.PasswordHash);
                    await userManager.AddToRoleAsync(student, "Student");
                }

                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.GradeTeachers.Any())
            {
                await _dbContext.GradeTeachers.AddRangeAsync(this.AddGradeParalelo());
                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.Categories.Any())
            {
                List<Category> categories = this.AddCategories();
                await _dbContext.Categories.AddRangeAsync(categories);
                await _dbContext.SaveChangesAsync();
            }

            //if (!_dbContext.Tests.Any())
            //{
            //    List<Test> tests = this.AddTests();
            //    await _dbContext.Tests.AddRangeAsync(tests);
            //    await _dbContext.SaveChangesAsync();
            //}
        }

        //private List<Test> AddTests()
        //{
        //    var tests = new List<Test>()
        //    {
        //         new Test
        //        {
        //            Date = DateTime.Now,
        //            LessonId = "40",
        //            Place = "Пловдив",
        //            Questions = this.AddQuestions("11"),
        //            TestId = "11",
        //            TestName = _dbContext.Lessons.First(l => l.LessonId == "40").Title,
        //        },
        //    };

        //    return tests;
        //}

        private ICollection<Question> AddQuestions(string testId)
        {
            var questions = new List<Question>();

            switch (testId)
            {
                case "11":
                    questions = new List<Question>()
            {
                new Question
                {
                    QuestionId = "1",
                    TestId = "40",
                    Title = "Модерното изкуство възниква във:",
                    Answers = this.AddAnswers("1"),
                },
                new Question
                {
                    QuestionId = "2",
                    TestId = "40",
                    Title = "Кой от художниците е виден представител на импресионизма?",
                    Answers = this.AddAnswers("2"),
                },
                new Question
                {
                    QuestionId = "3",
                    TestId = "40",
                    Title = "Индивидуален стил на художника е:",
                    Answers = this.AddAnswers("3"),
                },
                new Question
                {
                    QuestionId = "4",
                    TestId = "40",
                    Title = "Концептуалното изкуство включва:",
                    Answers = this.AddAnswers("4"),
                },
                new Question
                {
                    QuestionId = "5",
                    TestId = "40",
                    Title = "Електронните средства и технологии имат приложение в съвременното изкуство като:",
                    Answers = this.AddAnswers("5"),
                },
            };
                    break;
            }

            return questions;
        }

        private List<Answer> AddAnswers(string questionId)
        {
            var answers = new List<Answer>();

            switch (questionId)
            {
                case "1":
                    answers = new List<Answer>()
            {
                new Answer
                {
                    AnswerId = "1",
                    QuestionId = "1",
                    Title = "Франция",
                    IsCorrect = true,
                },
                new Answer
                {
                    AnswerId = "2",
                    QuestionId = "1",
                    Title = "Германия",
                },
                new Answer
                {
                    AnswerId = "3",
                    QuestionId = "1",
                    Title = "САЩ",
                },
                new Answer
                {
                    AnswerId = "4",
                    QuestionId = "1",
                    Title = "страна от Източна Европа",
                },
            };
                    break;
                case "2":
                    answers = new List<Answer>()
            {
                new Answer
                {
                    AnswerId = "5",
                    QuestionId = "2",
                    Title = "Моне",
                    IsCorrect = true,
                },
                new Answer
                {
                    AnswerId = "6",
                    QuestionId = "2",
                    Title = "Пикасо",
                },
                new Answer
                {
                    AnswerId = "7",
                    QuestionId = "2",
                    Title = "Ван Гог",
                },
                new Answer
                {
                    AnswerId = "8",
                    QuestionId = "2",
                    Title = "Гоген",
                },
            };
                    break;
                case "3":
                    answers = new List<Answer>()
            {
                new Answer
                {
                    AnswerId = "9",
                    QuestionId = "3",
                    Title = "Техника в живописа",
                    IsCorrect = true,
                },
                new Answer
                {
                    AnswerId = "10",
                    QuestionId = "3",
                    Title = "изображение с  материали и техники",
                },
                new Answer
                {
                    AnswerId = "11",
                    QuestionId = "3",
                    Title = "система на художника от закономерности за предаване на рисунък, форма, цветове, тема в творбата",
                },
                new Answer
                {
                    AnswerId = "12",
                    QuestionId = "3",
                    Title = "начин на представяне на творби в изложба",
                },
            };
                    break;
                case "4":
                    answers = new List<Answer>()
            {
                new Answer
                {
                    AnswerId = "13",
                    QuestionId = "4",
                    Title = "Артинсталации, пърформанс",
                    IsCorrect = true,
                },
                new Answer
                {
                    AnswerId = "14",
                    QuestionId = "4",
                    Title = "пърформанс, графити",
                },
                new Answer
                {
                    AnswerId = "15",
                    QuestionId = "4",
                    Title = "абстрактна живопис",
                },
                new Answer
                {
                    AnswerId = "16",
                    QuestionId = "4",
                    Title = "комикси, колажи",
                },
            };
                    break;
                case "5":
                    answers = new List<Answer>()
            {
                new Answer
                {
                    AnswerId = "17",
                    QuestionId = "5",
                    Title = "дигитално изкуство и видеоарт",
                    IsCorrect = true,
                },
                new Answer
                {
                    AnswerId = "18",
                    QuestionId = "5",
                    Title = "нямат приложение в съвременното изкуство",
                },
                new Answer
                {
                    AnswerId = "19",
                    QuestionId = "5",
                    Title = "техника в кубизма",
                },
                new Answer
                {
                    AnswerId = "20",
                    QuestionId = "5",
                    Title = "изразно средство в абстракционизма",
                },
            };
                    break;
            }

            return answers;
        }

        private List<Category> AddCategories()
        {
            var cathegories = new List<Category>
           {
               new Category()
               {
                   Id = 1.ToString(),
                   Title = "Живот и творчество",
                   Lessons = new List<Lesson>(this.AddLessons("1")),
                   SubjectId = _dbContext.Subjects.FirstOrDefault(s => s.Name == "Art")?.SubjectId,
               },
               new Category()
               {
                   Id = 2.ToString(),
                   Title = "Творци на модерното изкуство",
                   Lessons = new List<Lesson>(this.AddLessons("2")),
                   SubjectId = _dbContext.Subjects.FirstOrDefault(s => s.Name == "Art")?.SubjectId,
               },
               new Category()
               {
                   Id = 3.ToString(),
                   Title = "Световни музей и галерии",
                   Lessons = new List<Lesson>(this.AddLessons("3")),
                   SubjectId = _dbContext.Subjects.FirstOrDefault(s => s.Name == "Art")?.SubjectId,
               },
               new Category()
               {
                   Id = 4.ToString(),
                   Title = "Протестно Изкуство",
                   Lessons = new List<Lesson>(this.AddLessons("4")),
                   SubjectId = _dbContext.Subjects.FirstOrDefault(s => s.Name == "Art")?.SubjectId,
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
                        LessonId = "10",
                        Title = "Живот и творчество на художника",
                        Content = "„Днес може би малцина си дават точна сметка за онова, което изгубваме. Може би мнозина не знаят името на тоя извънредно даровит млад човек, отгледан в оскъдица от баща овчар. Но ще минат години и всички ще почувстваме цената на златото, което той носеше в душата си. И с щедростта на приказен цар разсипваше в своите декоративни видения” " +
                          "Сирак Скитник, в. „Слово”, 1927 г., бр. 1388.Днес може би малцина си дават точна сметка за онова, което изгубваме. Може би мнозина не знаят името на тоя извънредно даровит млад човек, отгледан в оскъдица от баща овчар. Но ще минат години и всички ще почувстваме цената на златото, което той носеше в душата си. И с щедростта на приказен цар разсипваше в своите декоративни видения” " +
                          "Сирак Скитник, в. „Слово”, 1927 г., бр. 1388",
                        CategoryId = id,
                        Level = 2,
                        IsUnlocked = false,
                    },
                    new Lesson
                    {
                        LessonId = "20",
                        Title = "Творчески процес",
                        Content = "Творчество e умствен и обществен процес на човешката дейност, при който се създават качествено нови материални и духовни ценности. Свързан е с генерирането на нови идеи и понятия, или пък нови връзки между вече съществуващи такива. Съществената разлика с производството е оригиналността на новия продукт. Творчеството е също така неразривно свързано със свободата.Творчество e умствен и обществен процес на човешката дейност, при който се създават качествено нови материални и духовни ценности. Свързан е с генерирането на нови идеи и понятия, или пък нови връзки между вече съществуващи такива. Съществената разлика с производството е оригиналността на новия продукт. Творчеството е също така неразривно свързано със свободата",
                        CategoryId = id,
                        Level = 1,
                        IsUnlocked = true,
                    },
                };
                    break;
                case "2":
                    lessons = new List<Lesson>()
                {
                    new Lesson
                    {
                        LessonId = "30",
                        Title = "Творци на модерното изкуство",
                        Content = "Първите крачки в модерното изкуство са направени от импресионистите и под влиянието на Едуард Мане през 1880-те години. Освен че отричат нормите на френската академия на изкуствата – нещо което преди тях са сторили и художниците на реализма – те добавят и съвсем нови елементи в рисуването. Примери за това са използването на чисти бои и рисуването чрез нанасяне на щрихи. Това отричане на класическото изкуство отваря пътя на художници като Сезан, Гоген и Ван Гог и към стилове като кубизма.Първите крачки в модерното изкуство са направени от импресионистите и под влиянието на Едуард Мане през 1880-те години. Освен че отричат нормите на френската академия на изкуствата – нещо което преди тях са сторили и художниците на реализма – те добавят и съвсем нови елементи в рисуването. Примери за това са използването на чисти бои и рисуването чрез нанасяне на щрихи. Това отричане на класическото изкуство отваря пътя на художници като Сезан, Гоген и Ван Гог и към стилове като кубизма",
                        CategoryId = id,
                        Level = 1,
                        IsUnlocked = true,
                    },
                    new Lesson
                    {
                        LessonId = "40",
                        Title = "Творци на съвременното изкуство",
                        Content = "Съвременното изкуство е съвременна живопис създадена в края на 20 век или в 21 век. Творчеството на съвременните живописци е повлияно от средата на глобален, културно разнообразен и технологично развиващ се свят. Съвременната живопис е динамична комбинация от материали, методи, идеи и субекти, които излизат изъвн традиционните граници на мисълта и излизат извън границите на човешкото въображение. Тази живопис е разнообразна и дигитална като цяло се отличава със самата липса на единен, организиращ принцип, идеология или „идеологизъм“. Съвременното изкуство е част от културен диалог, който засяга по-големи общностни рамки като лична и културна идентичност, семейство, общност и националност в света на чо.Съвременното изкуство е съвременна живопис създадена в края на 20 век или в 21 век. Творчеството на съвременните живописци е повлияно от средата на глобален, културно разнообразен и технологично развиващ се свят. Съвременната живопис е динамична комбинация от материали, методи, идеи и субекти, които излизат изъвн традиционните граници на мисълта и излизат извън границите на човешкото въображение. Тази живопис е разнообразна и дигитална като цяло се отличава със самата липса на единен, организиращ принцип, идеология или „идеологизъм“. Съвременното изкуство е част от културен диалог, който засяга по-големи общностни рамки като лична и културна идентичност, семейство, общност и националност в света на чо",
                        CategoryId = id,
                        Level = 2,
                        IsUnlocked = false,
                    },
                };
                    break;
                case "3":
                    lessons = new List<Lesson>()
                {
                    new Lesson
                    {
                        LessonId = "50",
                        Title = "Световни Музей",
                        Content = "1. Националния дворец музей, Тайпе, Тайван " +
                        "Този величествен музей се намира в Тайпе, Тайван, Китай. Tук ще намерите изложени над 693 507 експоната, изобразяващи древната история на Китай за последните 8000 години. На това място може да получите пълна представа за историята и културата на Китай." +
                        "2. Музея Прадо, Мадрид, Испания" +
                        "Ако се интересувате от историята и културата на Испания, трябва непременно да посетите музея Прадо. Тук ще имате възможността да разгледате много богата колекция от различни експонати от цяла Европа. Музеят разполага с около 950 склуптори, 64000 рисунки, 2400 щампи, 800 декоратишни изкуства, 900 монети и 800 медала...1. Националния дворец музей, Тайпе, Тайван " +
                        "Този величествен музей се намира в Тайпе, Тайван, Китай. Tук ще намерите изложени над 693 507 експоната, изобразяващи древната история на Китай за последните 8000 години. На това място може да получите пълна представа за историята и културата на Китай." +
                        "2. Музея Прадо, Мадрид, Испания" +
                        "Ако се интересувате от историята и културата на Испания, трябва непременно да посетите музея Прадо. Тук ще имате възможността да разгледате много богата колекция от различни експонати от цяла Европа. Музеят разполага с около 950 склуптори, 64000 рисунки, 2400 щампи, 800 декоратишни изкуства, 900 монети и 800 медала...",
                        CategoryId = id,
                        Level = 1,
                        IsUnlocked = true,
                    },
                    new Lesson
                    {
                        LessonId = "60",
                        Title = "Галерии",
                        Content = "От 1998 г. Берлинската картинна галерия се помества в специално построена за нея сграда в Културфорума. В проекта на новата музейна сграда, изпълнена от архитектите Хайнц Хилмер, Христоф Затлер и Томас Албрехт, е включена вилата на издателя Паул Парей. Северната фасада на правоъгълното здание е леко изтеглено напред. Цокълът на фасадата навява на италианския Ренесанс и пруския класицизъм.От 1998 г. Берлинската картинна галерия се помества в специално построена за нея сграда в Културфорума. В проекта на новата музейна сграда, изпълнена от архитектите Хайнц Хилмер, Христоф Затлер и Томас Албрехт, е включена вилата на издателя Паул Парей. Северната фасада на правоъгълното здание е леко изтеглено напред. Цокълът на фасадата навява на италианския Ренесанс и пруския класицизъм.",
                        CategoryId = id,
                        Level = 2,
                        IsUnlocked = false,
                    },
                };
                    break;
                case "4":
                    lessons = new List<Lesson>()
                {
                    new Lesson
                    {
                        LessonId = "70",
                        Title = "Художествени форми на протест",
                        Content = "Още преди две години обществените места в Истанбул бяха предвидени за творческите изяви на общо 88 творци от различни държави. Художествените инсталации и творби трябваше да се разположат из парка Гези, на площад Таксим, из застрашения от разрушаване квартал - т.е. все на места, за които дискутира цяла Турция. Истанбулската градска управа обаче не даде разрешение за ползването на обществените места като изложбени зали.Още преди две години обществените места в Истанбул бяха предвидени за творческите изяви на общо 88 творци от различни държави. Художествените инсталации и творби трябваше да се разположат из парка Гези, на площад Таксим, из застрашения от разрушаване квартал - т.е. все на места, за които дискутира цяла Турция. Истанбулската градска управа обаче не даде разрешение за ползването на обществените места като изложбени зали",
                        CategoryId = id,
                        Level = 1,
                        IsUnlocked =true,
                    },
                    new Lesson
                    {
                        LessonId = "80",
                        Title = "Форми на протестното изкуство",
                        Content = "Всяко разминаване с комунистическата идеология, включително в областта на изкуството, се преследва, отстранява, а в част от случаите се унищожава физически. Модерното изкуство се заклеймява като вражеско и човеконенавистно, и се обявява за продукт на психично болни и психопати. Забранено е внасянето на книги, албуми и каталози, както и организиране на изложби с потенциално модерен характер.Всяко разминаване с комунистическата идеология, включително в областта на изкуството, се преследва, отстранява, а в част от случаите се унищожава физически. Модерното изкуство се заклеймява като вражеско и човеконенавистно, и се обявява за продукт на психично болни и психопати. Забранено е внасянето на книги, албуми и каталози, както и организиране на изложби с потенциално модерен характер.",
                        CategoryId = id,
                        Level = 2,
                        IsUnlocked = false,
                    },
                };
                    break;
            }

            return lessons;
        }

        private GradeTeacher[] AddGradeParalelo()
        {
            GradeTeacher[] gradeParalelos = new GradeTeacher[4]
            {
                new GradeTeacher
                {
                   GradeTeacherId = "1",
                   GradeId = _dbContext.Grades.First(grade => grade.Name == "10a").GradeId,
                   TeacherId = _dbContext.Teachers.First(teacher => teacher.Email == "denis@denis.com").Id,
                },

                new GradeTeacher
                {
                    GradeTeacherId = "2",
                    GradeId = _dbContext.Grades.First(grade => grade.Name == "10e").GradeId,
                    TeacherId = _dbContext.Teachers.First(teacher => teacher.Email == "stam@stam.com").Id,
                },

                new GradeTeacher
                {
                    GradeTeacherId = "3",
                    GradeId = _dbContext.Grades.First(grade => grade.Name == "10b").GradeId,
                    TeacherId = _dbContext.Teachers.First(teacher => teacher.Email == "pesh@pesh.com").Id,
                },

                new GradeTeacher
                {
                    GradeTeacherId = "4",
                    GradeId = _dbContext.Grades.First(grade => grade.Name == "12a").GradeId,
                    TeacherId = _dbContext.Teachers.First(teacher => teacher.Email == "denis@denis.com").Id,
                },
            };
            return gradeParalelos;
        }

        private Student[] CreateStudents(ApplicationDbContext context)
        {
            Student[] students = new Student[12]
             {
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 099760043,
                    Name = "Ceco Ivanov",
                    PasswordHash = "ceco@ceco.com",
                    PlaceOfBirth = "Botevgrad",
                    Sex = "Male",
                    Telephone = 8765,
                    Address = "Stoicho Popov 12",
                    FatherName = "Atanas",
                    FatherMobileNumber = 09876554,
                    MotherName = "Stoika",
                    MotherMobileNumber = 099999933,
                    Email = "ceco@ceco.com",
                    UserName = "ceco@ceco.com",
                    GradeId = context.Grades.First(g => g.Name == "10b").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 094098321,
                    Name = "Ivailo Dimitrov",
                    PasswordHash = "ivailo@ivailo.com",
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
                    GradeId = context.Grades.First(g => g.Name == "10a").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 0978755442,
                    Name = "Mariq Ignatova",
                    PasswordHash = "maria@maria.com",
                    PlaceOfBirth = "Мелник",
                    Sex = "Female",
                    Telephone = 3300,
                    Address = "Stoicho Simeonov 17",
                    FatherName = "John Tailer",
                    FatherMobileNumber = 09870054,
                    MotherName = "Penka",
                    MotherMobileNumber = 0997699933,
                    Email = "maria@maria.com",
                    UserName = "maria@maria.com",
                    GradeId = context.Grades.First(g => g.Name == "10a").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 0978757342,
                    Name = "Атанас Сакъзов",
                    PasswordHash = "atanas@atanas.com",
                    PlaceOfBirth = "Мелник",
                    Sex = "Мале",
                    Telephone = 3400,
                    Address = "Радко Димитриев 19",
                    FatherName = "Свилен Сакъзов",
                    FatherMobileNumber = 098722054,
                    MotherName = "Генка",
                    MotherMobileNumber = 0987699033,
                    Email = "atanas@atanas.com",
                    UserName = "atanas@atanas.com",
                    GradeId = context.Grades.First(g => g.Name == "10b").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 099760043,
                    Name = "Stefan Conev",
                    PasswordHash = "stef@stef.com",
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
                    GradeId = context.Grades.First(g => g.Name == "10b").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 099760043,
                    Name = "Dragan Tconev",
                    PasswordHash = "drag@drag.com",
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
                    GradeId = context.Grades.First(g => g.Name == "10b").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 099760043,
                    Name = "Ивелин Тошев",
                    PasswordHash = "ivelin@ivelin.com",
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
                    GradeId = context.Grades.First(g => g.Name == "10b").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 099760043,
                    Name = "Маньо Минчев",
                    PasswordHash = "man@man.com",
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
                    GradeId = context.Grades.First(g => g.Name == "10a").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 099760043,
                    Name = "Щефан Сандерс",
                    PasswordHash = "shtef@shtef.com",
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
                    GradeId = context.Grades.First(g => g.Name == "10a").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 099760043,
                    Name = "Богомил Милев",
                    PasswordHash = "bogomil@bogomil.com",
                    PlaceOfBirth = "Елин Пелин",
                    Sex = "Male",
                    Telephone = 098765,
                    Address = "Княз Момчил 2",
                    FatherName = "Иван",
                    FatherMobileNumber = 09876554,
                    MotherName = "Стела",
                    MotherMobileNumber = 099999933,
                    Email = "bogomil@bogomil.com",
                    UserName = "bogomil@bogomil.com",
                    GradeId = context.Grades.First(g => g.Name == "10a").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 099760043,
                    Name = "Десислава Атанасова",
                    PasswordHash = "desi@desi.com",
                    PlaceOfBirth = "Благоевград",
                    Sex = "Female",
                    Telephone = 88899,
                    Address = "Богомил 2",
                    FatherName = "Светослав",
                    FatherMobileNumber = 09876554,
                    MotherName = "Биляна",
                    MotherMobileNumber = 099999933,
                    Email = "desi@desi.com",
                    UserName = "desi@desi.com",
                    GradeId = context.Grades.First(g => g.Name == "10a").GradeId,
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    MobilePhone = 099760043,
                    Name = "Радослав Първанов",
                    PasswordHash = "rad@rad.com",
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
                    GradeId = context.Grades.First(g => g.Name == "10a").GradeId,
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
                    Email = "denis@denis.com",
                    MobilePhone = 0997655443,
                    Name = "Denis Mihailov",
                    PasswordHash = "denis@denis.com",
                    PlaceOfBirth = "Plovdiv",
                    Sex = "Male",
                    Telephone = 3344,
                    UserName = "denis@denis.com",
                },

                new Teacher
                {
                    Id = Guid.NewGuid().ToString(),
                    DateOfBirth = DateTime.Parse("05/2/1987"),
                    Email = "stam@stam.com",
                    MobilePhone = 099456373,
                    Name = "Stamat Ionchev",
                    PasswordHash = "stam@stam.com",
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
                    PasswordHash = "pesh@pesh.com",
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
            var paraleloArray = new string[8] { "a", "b", "c", "d", "e", "f", "g", "h" };
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
                            "Mathematics", "Bulgarian language", "Literature",
                            "Geography", "History", "Computer Graphics",
                            "Art",
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

        public async Task CreateRoles()
        {
            var roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = _serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var context = _serviceProvider.GetRequiredService<ApplicationDbContext>();

            bool isUserAddedInRole = await roleManager.RoleExistsAsync("Admin");

            if (!isUserAddedInRole)
            {
                // Create Admin role
                var role = new IdentityRole();
                role.Name = "Admin";
                await roleManager.CreateAsync(role);
            }

            if (!context.Users.Any(u => u.Email == "admin@admin.com"))
            {
                var user = new IdentityUser();
                user.UserName = _configuration.GetSection("AdminConfig:Username").Value;
                user.Email = _configuration.GetSection("AdminConfig:Email").Value;
                var userPassword = _configuration.GetSection("AdminConfig:Password").Value;

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
