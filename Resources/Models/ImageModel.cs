using Resources.Models.Base;

namespace Resources.Models
{
    public class ImageModel : BaseModel
    {
        public string ImageAsString { get; set; } = default!;
        public bool IsPublishedOnSitePage { get; set; }
    }
}
