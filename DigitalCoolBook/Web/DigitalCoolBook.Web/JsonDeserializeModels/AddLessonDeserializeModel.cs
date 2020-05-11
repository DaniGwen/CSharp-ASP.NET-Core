namespace DigitalCoolBook.App.JsonDeserializeModels
{
    using Microsoft.AspNetCore.Mvc;

    public class AddLessonDeserializeModel
    {
        [FromForm]
        public string title { get; set; }

        [FromForm]
        public string contend { get; set; }

        [FromForm]
        public string categoryId { get; set; }
    }
}
