namespace DigitalCoolBook.App.Models.CategoryViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class LessonEditViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage ="Моля въведете заглавие.")]
        public string Title { get; set; }

        [Required(ErrorMessage ="Моля въведете съдържание.")]
        public string Content { get; set; }

        public IList<CategoryAjaxViewModel> Categories { get; set; }

        public string CategoryId { get; set; }
    }
}
