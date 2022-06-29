using Resources.Models.Base;

namespace Resources.Models
{
    public class BlogModel : BaseModel
    {
        public string BlogTitle { get; set; } = default!;
        public string ShortBlogDescription { get; set; } = default!;
        public string LongBlogDescription { get; set; } = default!;
        public DateTime PublicationDate { get; set; }
        public string BlogImageAsString { get; set; } = default!;
    }
}
