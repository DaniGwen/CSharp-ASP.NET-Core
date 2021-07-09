using System;
using System.Collections.Generic;

namespace DigitalCoolBook.Models
{
    public class TestRoom
    {
        public TestRoom()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Students = new List<TestRoomStudent>();
        }

        public string Id { get; set; }

        public List<TestRoomStudent> Students { get; set; }

        public string TeacherId { get; set; }

        public string TestId { get; set; }
    }
}
