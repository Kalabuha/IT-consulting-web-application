using Resources.Models.Base;

namespace Resources.Models
{
    public class HeaderModel : BaseModel
    {
        public string Main { get; set; } = default!;
        public string Services { get; set; } = default!;
        public string Projects { get; set; } = default!;
        public string Blogs { get; set; } = default!;
        public string Contacts { get; set; } = default!;

        public string Slogan { get; set; } = default!;
    }
}
