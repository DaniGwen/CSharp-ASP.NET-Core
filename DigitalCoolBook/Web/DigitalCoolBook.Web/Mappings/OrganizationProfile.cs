namespace DigitalCoolBook.Services.Mapping
{
    using AutoMapper;
    using DigitalCoolBook.App.Models.CategoryViewModels;
    using DigitalCoolBook.App.Models.GradeParaleloViewModels;
    using DigitalCoolBook.App.Models.GradesViewModels;
    using DigitalCoolBook.App.Models.StudentViewModels;
    using DigitalCoolBook.App.Models.SubjectViewModels;
    using DigitalCoolBook.App.Models.TeacherViewModels;
    using DigitalCoolBook.App.Models.TestviewModels;
    using DigitalCoolBook.Models;
    using DigitalCoolBook.Web.Models.TestviewModels;

    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            // Student mappings
            this.CreateMap<Student, StudentEditViewModel>();
            this.CreateMap<StudentEditViewModel, Student>();
            this.CreateMap<StudentRegisterInputModel, Student>();
            this.CreateMap<Student, StudentDetailsViewModel>();
            this.CreateMap<Student, StudentChangePasswordViewModel>();
            this.CreateMap<Student, StudentTestDropDownModel>();

            // Grade mappings
            this.CreateMap<Grade, GradeViewModel>();
            this.CreateMap<ParaleloCreateViewModel, GradeTeacher>();
            this.CreateMap<GradeTeacher, ParaleloCreateViewModel>();

            // Teacher mappings
            this.CreateMap<TeacherRegisterModel, Teacher>();
            this.CreateMap<Teacher, TeacherDetailsViewModel>();
            this.CreateMap<TeacherDetailsViewModel, Teacher>();
            this.CreateMap<Teacher, TeacherEditViewModel>();
            this.CreateMap<Teacher, TeacherChangePasswordViewModel>();

            // Subjects mappings
            this.CreateMap<Subject, SubjectViewModel>();
            this.CreateMap<Lesson, LessonsViewModel>();
            this.CreateMap<Category, CategoryCreateViewModel>();
            this.CreateMap<Category, CategoryAjaxViewModel>();
            this.CreateMap<Lesson, LessonEditViewModel>();
            this.CreateMap<Category, CategoryViewModel>();
            this.CreateMap<Lesson, LessonPreviewViewModel>();

            // Test mappings
            this.CreateMap<Test, TestViewModel>();
            this.CreateMap<TestViewModel, Test>();
            this.CreateMap<Test, TestsNamesViewModel>();
            this.CreateMap<Test, TestStartViewModel>();
            this.CreateMap<Test, ExpiredTest>();
            this.CreateMap<Test, SetTimerViewModel>();
            this.CreateMap<Test, TestPreviewViewModel>();
            this.CreateMap<Test, TestDetailsViewModel>();
            this.CreateMap<Test, ActiveTestsViewModel>();

            // Question mappings
            this.CreateMap<Question, QuestionsModel>();
            this.CreateMap<Question, QuestionDetailsViewModel>();

            // Answer mappings
            this.CreateMap<Answer, AnswerDetailsViewModel>();
        }
    }
}
