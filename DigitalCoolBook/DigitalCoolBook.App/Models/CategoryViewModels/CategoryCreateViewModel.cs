namespace DigitalCoolBook.App.Models.CategoryViewModels
{
    using DigitalCoolBook.Models;
    using System.Collections.Generic;

    public class CategoryCreateViewModel
    {
        public string CategoryId { get; set; }

        public string SubjectId { get; set; }

        public string Title { get; set; }

        public string Name { get; set; }

        public Lesson Lesson { get; set; }

        public List<Category> Categories { get; set; }

        public List<Subject> Subjects { get; set; }
    }
}
