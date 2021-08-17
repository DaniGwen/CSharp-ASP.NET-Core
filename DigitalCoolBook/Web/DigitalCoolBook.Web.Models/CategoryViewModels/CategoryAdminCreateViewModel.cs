using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DigitalCoolBook.App.Models.SubjectViewModels;

namespace DigitalCoolBook.Web.Models.CategoryViewModels
{
    public class CategoryAdminCreateViewModel
    {
        [Required(ErrorMessage = "Please enter title")]
        public string Title { get; set; }

        [Required]
        public string SubjectId { get; set; }

        public List<SubjectViewModel> Subjects { get; set; }
    }
}
