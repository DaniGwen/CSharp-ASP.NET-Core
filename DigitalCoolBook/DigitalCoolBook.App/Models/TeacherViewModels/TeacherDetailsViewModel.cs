namespace DigitalCoolBook.App.Models.TeacherViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class TeacherDetailsViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage="Попълнете полето.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Попълнете полето.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Попълнете полето.")]
        public string DateOfBirth { get; set; }

        [Required(ErrorMessage = "Попълнете полето.")]
        public string PlaceOfBirth { get; set; }

        [Required(ErrorMessage = "Попълнете полето.")]
        public string Sex { get; set; }

        public int? MobilePhone { get; set; }

        public int Telephone { get; set; }

        public string Username { get; set; }
    }
}
