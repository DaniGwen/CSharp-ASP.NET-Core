namespace DigitalCoolBook.App.Models.TeacherViewModels
{
    using System;

    public class TeacherDetailsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PlaceOfBirth { get; set; }

        public string Sex { get; set; }

        public int? MobilePhone { get; set; }

        public int Telephone { get; set; }

        public string Username { get; set; }
    }
}
