namespace DigitalCoolBook.App.Models.SubjectViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class SubjectCreateViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Заглавието трябва да е минимум 3 букви", MinimumLength = 3)]
        public string Name { get; set; }
    }
}
