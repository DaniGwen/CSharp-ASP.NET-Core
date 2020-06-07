using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalCoolBook.Models
{
    public class TestRoomStudent
    {
        public TestRoomStudent()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string StudentId { get; set; }

        public string TestRoomId { get; set; }

        [ForeignKey("TestRoomId")]
        public TestRoom TestRoom { get; set; }

        public bool Finished { get; set; }
    }
}
