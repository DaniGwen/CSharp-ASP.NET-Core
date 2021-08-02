using AspNetCoreHero.ToastNotification.Abstractions;

namespace DigitalCoolBook.App.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using Hubs;
    using Models.CategoryViewModels;
    using Models.TestviewModels;
    using DigitalCoolBook.Models;
    using DigitalCoolBook.Services.Contracts;
    using DigitalCoolBook.Web.Models.TestviewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class TestController : Controller
    {
        private readonly ITestService _testService;
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;
        private readonly IGradeService _gradeService;
        private readonly IUserService _userService;
        private readonly IQuestionService _questionService;
        private readonly IScoreService _scoreService;
        private readonly IHubContext<TestHub> _testHub;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly INotyfService _toasterService;

        public TestController(
            ITestService testService,
            ISubjectService subjectService,
            IMapper mapper,
            IGradeService gradeService,
            IUserService userService,
            IQuestionService questionService,
            IScoreService scoreService,
            IHubContext<TestHub> testHub,
            UserManager<IdentityUser> userManager,
            INotyfService toasterService
           )
        {
            _testService = testService;
            _subjectService = subjectService;
            _mapper = mapper;
            _gradeService = gradeService;
            _userService = userService;
            _questionService = questionService;
            _scoreService = scoreService;
            _testHub = testHub;
            _userManager = userManager;
            _toasterService = toasterService;
        }

        // Admin creates a test for a lesson
        [HttpGet]
        [Authorize(Roles = "Admin, Teacher")]
        public IActionResult CreateTest()
        {
            var lessons = _subjectService.GetLessons().ToList();

            var model = new TestCreateAdminViewModel()
            {
                Lessons = _mapper.Map<List<LessonsViewModel>>(lessons),
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Teacher")]
        [ActionName("CreateTest")]
        public async Task<IActionResult> CreateTestAsync(ICollection<QuestionAnswerViewModel> model,
            string lessonId,
            string place)
        {
            var lesson = await _subjectService.GetLessonAsync(lessonId);

            // Instantiate test
            var test = new Test
            {
                TestId = Guid.NewGuid().ToString(),
                LessonId = lessonId,
                Place = place,
                TestName = lesson.Title,
            };

            // Collections of answers and questions to be saved in DB
            var questionsForDb = new List<Question>();
            var answersForDb = new List<Answer>();

            // Create question and answers
            foreach (var questionDto in model)
            {
                var question = new Question
                {
                    QuestionId = Guid.NewGuid().ToString(),
                    TestId = test.TestId,
                    Title = questionDto.Question,
                };

                questionsForDb.Add(question);

                // Setting answers properties
                foreach (var answerDto in questionDto.Answers)
                {
                    var answer = new Answer
                    {
                        AnswerId = Guid.NewGuid().ToString(),
                        Title = answerDto,
                        QuestionId = question.QuestionId,
                    };

                    answersForDb.Add(answer);
                }
            }

            await _testService.AddTestAsync(test);
            await _questionService.AddQuestionsAsync(questionsForDb);
            await _questionService.AddAnswersAsync(answersForDb);

            return this.RedirectToAction("MarkCorrectAnswers", new { testId = test.TestId });
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Teacher")]
        public IActionResult MarkCorrectAnswers(string testId)
        {
            var questions = _questionService
                .GetQuestions()
                .Where(question => question.TestId == testId);

            var model = _mapper.Map<List<QuestionsModel>>(questions);

            // Passing test Id to the view
            this.TempData["TestId"] = testId;

            // Add answers to questions
            foreach (var question in model)
            {
                question.Answers = _questionService.GetAnswers()
               .Where(answer => answer.QuestionId == question.QuestionId)
               .ToList();
            }

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Teacher")]
        [ActionName("MarkCorrectAnswers")]
        public async Task<IActionResult> MarkCorrectAnswersAsync(string[] correctAnswerIds, string testId)
        {
            try
            {
                // Sets correct answers to true
                foreach (var correctAnswer in correctAnswerIds)
                {
                    var answer = await _questionService.GetAnswerAsync(correctAnswer);
                    answer.IsCorrect = true;
                }

                await _questionService.SaveChangesAsync();

                return this.Json("Test was successfully saved");
            }
            catch (Exception)
            { 
                return this.Json(new { error = "Test was not saved"});
            }
        }

        [Authorize(Roles = "Teacher, Admin")]
        public JsonResult GetStudents(string gradeId)
        {
            var students = this._userService.GetStudents()
                .Where(s => s.GradeId == gradeId)
                .ToList();

            return this.Json(students);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult Tests()
        {
            var teacherId = this._userManager.GetUserId(this.User);
            var tests = this._testService.GetTests().ToList();
            var activeTests = this._testService.GetActiveTestsByTeacherId(teacherId);

            // Map and create model for the view
            var viewModel = this._mapper.Map<List<TestsNamesViewModel>>(tests);

            foreach (var test in viewModel)
            {
                if (activeTests.Any(x => x.TestId == test.TestId))
                {
                    test.IsActive = true;
                }
            }

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult SetTestTimer(string testId)
        {
            var teacherId = _userManager.GetUserId(this.User);
            var activeTests = _testService.GetActiveTestsByTeacherId(teacherId);

            if (activeTests.Any(x => x.TestId == testId))
                return RedirectToAction("StartTest", new { id = testId });

            var model = new TestViewModel
            {
                Grades = _gradeService
                .GetGrades()
                .OrderBy(g => g.Name)
                .ToList(),
                TestId = testId,
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> SetTestTimerAsync(TestViewModel model)
        {
            var teacherId = _userManager.GetUserId(this.User);

            try
            {
                if (model.Students == null || !model.Students.Any())
                {
                    this.ModelState.AddModelError(string.Empty, "Select at least one participant");

                    return this.View(new TestViewModel
                    {
                        Grades = _gradeService
                            .GetGrades()
                            .OrderBy(g => g.Name)
                            .ToList(),
                        TestId = model.TestId,
                    });
                }

                var test = await _testService.GetTestAsync(model.TestId);
                test.TeacherId = teacherId;
                test.Timer = model.Timer;

                var testStudents = new List<TestStudent>();
                var studentsDb = this._userService.GetStudents().ToList();

                // Adding "TestStudent" relation
                foreach (var studentName in model.Students)
                {
                    var student = studentsDb.FirstOrDefault(s => s.Name == studentName);
                    var testStudent = new TestStudent()
                    {
                        StudentId = student?.Id,
                        Student = student,
                        TestId = test.TestId,
                        Test = test
                    };

                    testStudents.Add(testStudent);
                }

                await _testService.AddTestRoomAsync(model.Students, test.TeacherId, model.TestId);
                await _testService.AddTestStudentsAsync(testStudents);

                return this.RedirectToAction("StartTest", "Test", new { id = test.TestId });
            }
            catch (Exception exception)
            {
                this.TempData["ErrorMsg"] = exception.Message;

                return this.Redirect("/Home/Error");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Teacher, Student")]
        public async Task<IActionResult> StartTest([FromRoute] string id)
        {
            try
            {
                var test = await _testService.GetTestAsync(id);

                // Keep the test Id for EndTest action
                this.TempData["TestId"] = test?.TestId;

                var testViewModel = _mapper.Map<TestStartViewModel>(test);

                // Gets the participating students
                testViewModel.StudentNames = await _testService
                    .GetStudentsInTestRoomAsync(test?.TestId);

                // Gets the questions for this test
                var questionsDb = _questionService
               .GetQuestions()
               .Where(question => question.TestId == test.TestId)
               .ToList();

                // Add questions to model and map questions to QuestionModel
                testViewModel.Questions
                        .AddRange(_mapper.Map<List<QuestionsModel>>(questionsDb));

                // Add Answers to Questions for this test
                foreach (var question in testViewModel.Questions)
                {
                    var answers = _questionService.GetAnswers()
                        .Where(answer => answer.QuestionId == question.QuestionId)
                        .ToList();

                    question.Answers.AddRange(answers);
                }

                return this.View(testViewModel);
            }
            catch (Exception)
            {
                _toasterService.Error("Error, something went wrong");

                this.Redirect("/Home/Index");
            }

            return this.View();
        }

        [HttpPost]
        [ActionName("EndTest")]
        [Authorize(Roles = "Teacher, Student")]
        public async Task<IActionResult> EndTestAsync(ICollection<EndTestViewModel> model)
        {
            int score = 0;

            var testId = this.TempData["TestId"].ToString();

            var testDb = _testService
                .GetTests()
                .FirstOrDefault(x => x.TestId == testId);

            var newArchivedTest = _mapper.Map<ArchivedTestViewModel>(testDb);

            await _testService.AddArchivedTest(newArchivedTest);

            if (this.User.IsInRole("Student"))
            {
                score = await this.ProcessTestAsync(model, testId);
            }

            this.ViewData["Result"] = score;

            // Check if all students in the test room has finished
            bool isAllFinished = _testService.CheckAllFinished();

            if (!isAllFinished)
            {
                // Test room is empty so we remove it along with the students in it
                await _testService.RemoveTestRoomAsync(testId);
            }

            if (this.User.IsInRole("Teacher"))
            {
                return this.Redirect("/Home/Index");
            }

            return this.View("Result");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddQuestions()
        {
            var lessonsDb = this._subjectService.GetLessons()
                .OrderBy(lesson => lesson.Title)
                .ToList();

            var lessonsDto = this._mapper.Map<List<LessonsViewModel>>(lessonsDb);

            var model = new QuestionsAddViewModel
            {
                Lessons = lessonsDto,
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("AddQuestions")]
        public async Task<IActionResult> AddQuestionsAsync(QuestionsAddViewModel model)
        {
            try
            {
                if (model.Questions.Count < 1)
                {
                    this.ModelState.AddModelError(string.Empty, "Please add questions");

                    return this.View(model);
                }

                var test = _testService
                    .GetTests()
                    .Include("Lesson")
                    .FirstOrDefault(t => t.LessonId == model.LessonId);
                if (test == null)
                {
                    _toasterService.Error("Test not found");

                    return this.RedirectToAction("AddQuestions");
                }
                foreach (var question in model.Questions)
                {
                    test.Questions.Add(new Question
                    {
                        QuestionId = Guid.NewGuid().ToString(),
                        Title = question,
                    });
                }
            }
            catch (Exception)
            {
               _toasterService.Error("Error, something went wrong");

                return this.Redirect("/Home/Index");
            }

            await _testService.SaveChangesAsync();
            _toasterService.Success("Questions were added");

            return this.Redirect("/Home/Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CheckTestExist(string lessonId)
        {
            var test = _testService.GetTests()
               .FirstOrDefault(t => t.LessonId == lessonId);

            if (test == null)
            {
                return this.BadRequest();
            }

            return this.Json(string.Empty);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult GetLessons(string lessonId)
        {
            //TODO
            return this.Json(string.Empty);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult TestsPreview()
        {
            var tests = _testService.GetTests();

            var model = _mapper.Map<List<TestPreviewViewModel>>(tests);

            return this.View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ActionName("GetTestDetails")]
        public async Task<IActionResult> GetTestDetailsAsync(string id)
        {
            try
            {
                var test = await this._testService.GetTestAsync(id);

                // Map test to view model
                var model = this._mapper.Map<TestDetailsViewModel>(test);

                // Getting the questions for this test
                var questions = this._questionService.GetQuestions()
                    .Where(question => question.TestId == model.TestId)
                    .ToList();

                // Getting the answers from DB
                var answers = this._questionService.GetAnswers().ToList();

                // Map the questions to questions view model
                var questionsModel = this._mapper.Map<List<QuestionDetailsViewModel>>(questions);

                // Iterate through questionsModel and add answers
                foreach (var question in questionsModel)
                {
                    // Filter the answers for that question
                    var answersForQuestion = answers.Where(answer => answer.QuestionId == question.QuestionId)
                        .ToList();

                    var answersModel = this._mapper.Map<List<AnswerDetailsViewModel>>(answersForQuestion);

                    question.Answers = answersModel;
                }

                model.Questions = questionsModel;

                return this.View(model);
            }
            catch (Exception)
            {
                return this.Json("Something gone wrong..");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("DeleteTest")]
        public async Task<IActionResult> DeleteTestAsync(string testId)
        {
            try
            {
                await _testService.RemoveTestAsync(testId);

                return this.Json("Test is deleted.");
            }
            catch (Exception)
            {
                return this.Json("Error deleting test!");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ActionName("EditTest")]
        public async Task<IActionResult> EditTestAsync(string id)
        {
            try
            {
                var test = await this._testService.GetTestAsync(id);

                // Map test to view model
                var model = this._mapper.Map<TestDetailsViewModel>(test);

                // Getting the questions for this test
                var questions = this._questionService.GetQuestions()
                    .Where(question => question.TestId == model.TestId)
                    .ToList();

                // Getting the answers from DB
                var answers = this._questionService.GetAnswers().ToList();

                // Map the questions to questions view model
                var questionsModel = this._mapper.Map<List<QuestionDetailsViewModel>>(questions);

                // Iterate through questionsModel and add answers
                foreach (var question in questionsModel)
                {
                    // Filter the answers for that question
                    var answersForQuestion = answers.Where(answer => answer.QuestionId == question.QuestionId)
                        .ToList();

                    // Map answers to answers view model
                    var answersModel = this._mapper.Map<List<AnswerDetailsViewModel>>(answersForQuestion);

                    question.Answers = answersModel;
                }

                model.Questions = questionsModel;

                return this.View(model);
            }
            catch (Exception)
            {
                return this.Json("Error editing test.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("EditTest")]
        public async Task<IActionResult> EditTestAsync(ICollection<TestEditViewModel> model, string testId)
        {
            try
            {
                // questions for test from DB
                var questions = this._questionService.GetQuestions()
                    .Where(q => q.TestId == testId)
                    .ToList();

                // answers from DB
                var answers = this._questionService.GetAnswers()
                    .ToList();

                var answersForDb = new List<Answer>();

                foreach (var question in questions)
                {
                    foreach (var questionModel in model)
                    {
                        // Set question title
                        if (question.QuestionId == questionModel.QuestionId)
                        {
                            question.Title = questionModel.Question;

                            // Find the answers belong to this question
                            var answersToRemove = answers
                                .Where(a => a.QuestionId == question.QuestionId)
                                .ToList();

                            // Remove old answers
                            await this._questionService.RemoveAnswers(answersToRemove);

                            // Create new answers with updated titles
                            foreach (var answerTitle in questionModel.Answers)
                            {
                                var answer = new Answer
                                {
                                    AnswerId = Guid.NewGuid().ToString(),
                                    QuestionId = question.QuestionId,
                                    Title = answerTitle,
                                };

                                answersForDb.Add(answer);
                            }
                        }
                    }
                }

                await _questionService.AddAnswersAsync(answersForDb);

                // Redirect to MarkCorrectAnswers action to select the right answers
                return this.RedirectToAction("MarkCorrectAnswers", new { testId = testId });
            }
            catch (Exception)
            {
                _toasterService.Error("Error saving the changes");
                return this.RedirectToAction("EditTest", new { id = testId });
            }
        }

        private async Task<int> ProcessTestAsync(ICollection<EndTestViewModel> model, string testId)
        {
            var studentId = _userManager.GetUserId(this.User);

            // Gets questions for this test
            var questions = _questionService
                .GetQuestions()
                .Include(question => question.Answers)
                .Where(question => question.TestId == testId)
                .ToList();

            // Max points 100
            var score = 0;

            // Check for correct answer and add points
            foreach (var question in questions)
            {
                var correctAnswerId = question.Answers
                    .First(answer => answer.IsCorrect).AnswerId;

                var modelAnswerId = model.First(q => q.QuestionId == question.QuestionId).AnswerId;

                if (modelAnswerId == correctAnswerId)
                {
                    score += 10;
                }
            }

            // Get Test from DB
            var test = await _testService
                .GetTestAsync(testId);

            // Create expired test for history
            var newExpiredTest = _mapper.Map<ExpiredTest>(test);
            newExpiredTest.Date = DateTime.UtcNow;
            newExpiredTest.Result = score;
            newExpiredTest.StudentId = studentId;
            newExpiredTest.ExpiredTestId = test.TestId;

            // Add expired test to DB if score is bigger then the one in the database
            if (_testService.GetExpiredTests().Any(x => x.StudentId == studentId && x.ExpiredTestId == testId))
            {
                var expiredTestDb = await _testService.GetExpiredTests()
                    .FirstOrDefaultAsync(x => x.StudentId == studentId && x.ExpiredTestId == testId);

                if (expiredTestDb?.Result < newExpiredTest.Result)
                {
                    await _testService.RemoveExpiredTest(expiredTestDb.ExpiredTestId, expiredTestDb.StudentId);
                    await _testService.AddExpiredTestAsync(newExpiredTest);
                }
            }
            else
            {
                await _testService.AddExpiredTestAsync(newExpiredTest);
            }

            // Create Score
            if (this.User.IsInRole("Student"))
            {
                // Create Score for student
                var scoreId = await _scoreService.CreateScoreAsync(score, test.LessonId);

                // Create ScoreStudent
                await _scoreService.CreateScoreStudentAsync(scoreId, studentId);
            }

            // Set the student score property and display it after all students has finished in teacher's view
            await _testService.TestRoomStudentFinished(studentId, score);

            return score;
        }

        [HttpGet]
        [Authorize(Roles = "Student")]
        public ActionResult IsStudentInTest()
        {
            var studentId = this.User
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value;

            var testId = this._testService.IsStudentInTest(studentId);

            var testRoomStudent = this._testService.GetTestRoomStudent(studentId);

            if (testId != null && testRoomStudent.Finished == false)
            {
                var testName = this._testService
                    .GetTests()
                    .First(x => x.TestId == testId)
                    .TestName;

                return this.Json(new
                {
                    success = true,
                    testId = testId,
                    testName = testName,
                });
            }

            return this.Json(new
            {
                success = false,
                Message = "No active tests.",
            });
        }

        [Authorize(Roles = "Teacher")]
        [ActionName("EndTestAllStudents")]
        public async Task<IActionResult> EndTestAllStudentsAsync(string testId)
        {
            await _testHub.Clients.All.SendAsync("SubmitAll");

            var testDb = _testService
                .GetTests()
                .First(x => x.TestId == testId);

            testDb.IsExpired = true;
           
            await _testService.RemoveTestAsync(testDb.TestId);
            await _testService.RemoveTestRoomAsync(testDb.TestId);

            return this.Redirect("/Home/Index");
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult ActiveTests()
        {
            var teacherId = _userManager.GetUserId(this.User);
            List<Test> activeTests = _testService.GetActiveTestsByTeacherId(teacherId);

            var model = _mapper.Map<List<ActiveTestsViewModel>>(activeTests);

            return this.View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> TestsHistory()
        {
            var teacherId = _userManager.GetUserId(this.User);

            var tests = await _testService.GetArchivedTestsByTeacherId(teacherId);

            return this.View(tests);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult<List<StudentTestSummaryViewModel>>> TestSummary(string testId)
        {
            this.ViewData["testId"] = testId;
            var teacherId = _userManager.GetUserId(this.User);

            var archivedTests = await _testService.GetExpiredTests()
                .Where(x => x.TeacherId == teacherId && x.ExpiredTestId == testId)
                .ToListAsync();

            var studentTestSummaryViewModels = archivedTests
                .Select(x => new StudentTestSummaryViewModel
                {
                    Score = x.Result,
                    StudentName = _userService.GetStudentAsync(x.StudentId).Result.Name,
                    TestId = x.ExpiredTestId,
                    TestName = x.TestName
                })
                .ToList();

            return this.View(studentTestSummaryViewModels);
        }
    }
}