namespace DigitalCoolBook.Models
{
    public class Answer
    {
        public string AnswerId { get; set; }

        public string Title { get; set; }

        public string QuestionId { get; set; }

        public Question Question { get; set; }

        public bool IsChecked { get; set; }

        public bool IsCorrect { get; set; }
    }
}
