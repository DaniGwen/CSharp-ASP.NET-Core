namespace DigitalCoolBook.Models
{
    public class ScoreStudent
    {
        public int ScoreRecordId { get; set; }

        public string ScoreId { get; set; }
        public Score Score { get; set; }

        public string StudentId { get; set; }
        public Student Student { get; set; }
    }
}