using System;

namespace KniveGallery.Web.Models
{
    public class ImageFile
    {
        public string Name { get; set; }

        public int Size { get; set; }

        public string Type { get; set; }

        public string LastModified { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}
