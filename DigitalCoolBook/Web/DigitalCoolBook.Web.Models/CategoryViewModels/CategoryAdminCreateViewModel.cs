namespace DigitalCoolBook.App.Models.CategoryViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DigitalCoolBook.App.Models.SubjectViewModels;

    public class CategoryAdminCreateViewModel
    {
        [Required(ErrorMessage = "Моля въведете заглавие.")]
        public string Title { get; set; }

        [Required]
        public string SubjectId { get; set; }

        public List<SubjectViewModel> Subjects { get; set; }
    }
}
