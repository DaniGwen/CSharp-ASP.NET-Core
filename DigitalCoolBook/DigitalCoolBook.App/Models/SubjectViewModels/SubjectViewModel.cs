namespace DigitalCoolBook.App.Models.SubjectViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class SubjectViewModel
    {
        [Key]
        public string SubjectId { get; set; }

        public string Name { get; set; }
    }
}
