using System.Collections.Generic;

namespace DigitalCoolBook.Models
{
    public class Cathegory
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public List<Lesson> Lessons { get; set; }
    }
}