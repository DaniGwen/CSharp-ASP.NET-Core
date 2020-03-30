namespace DigitalCoolBook.App.Models.SubjectViewModels
{
    using System.Collections.Generic;
    using DigitalCoolBook.Models;

    public class SubjectViewModel
    {
        public string SubjectId { get; set; }

        public string CategoryId { get; set; }

        public string CategoryTitle { get; set; }

        public string Name { get; set; }

        public List<Category> Categories { get; set; }
    }
}
