namespace DigitalCoolBook.App.Models.CategoryViewModels
{
    using System.Collections.Generic;

    public class LessonEditViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public IList<CategoryAjaxViewModel> Categories { get; set; }
    }
}
