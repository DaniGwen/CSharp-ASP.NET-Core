using Microsoft.AspNetCore.Mvc;

namespace DigitalCoolBook.App.JsonDeserializeModels
{
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
