namespace DigitalCoolBook.App.Models.CategoryViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DigitalCoolBook.Models;

    public class CategoryCreateViewModel
    {
        [Required(ErrorMessage = "Моля изберете тема.")]
        public string CategoryId { get; set; }

        [Required(ErrorMessage = "Моля изберете предмет.")]
        public string SubjectId { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<Subject> Subjects { get; set; }
    }
}
