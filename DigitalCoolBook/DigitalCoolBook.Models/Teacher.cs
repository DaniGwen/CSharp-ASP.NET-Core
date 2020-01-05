using System;

namespace DigitalCoolBook.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PlaceOfBirth { get; set; }

        public string Sex { get; set; }

        public int MobilePhone { get; set; }

        public int Telephone { get; set; }

        public string Password { get; set; }
    }
}