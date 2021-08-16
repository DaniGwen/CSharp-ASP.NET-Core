using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DigitalCoolBook.App.Models.CategoryViewModels;

namespace DigitalCoolBook.Web.Models.CategoryViewModels
{
    public class LessonEditViewModel
    {
        public string LessonId { get; set; }

        [Required(ErrorMessage ="Enter title")]
        [StringLength(maximumLength: 60, ErrorMessage = "Minimum 3 symbols, maximum 60", MinimumLength = 3)]
        public string Title { get; set; }

        [Required(ErrorMessage ="Enter content")]
        [StringLength(maximumLength: 20000, ErrorMessage = "Minimum 20 symbols", MinimumLength = 20)]
        public string Content { get; set; }

        public IList<CategoryViewModel> Categories { get; set; }

        public string CategoryId { get; set; }
    }
}
