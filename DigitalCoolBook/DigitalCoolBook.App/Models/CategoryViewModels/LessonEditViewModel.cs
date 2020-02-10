namespace DigitalCoolBook.App.Models.CategoryViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class LessonEditViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage ="Въведете заглавие")]
        [StringLength(maximumLength: 40, ErrorMessage = "Въведете заглавие до 40 символа и минимум 3 символа.", MinimumLength = 3)]
        public string Title { get; set; }

        [Required(ErrorMessage ="Въведете съдържание")]
        [StringLength(maximumLength: 1000, ErrorMessage = "Минимално съдържание 20 символа.", MinimumLength = 20)]
        public string Content { get; set; }

        public IList<CategoryViewModel> Categories { get; set; }

        public string CategoryId { get; set; }
    }
}
