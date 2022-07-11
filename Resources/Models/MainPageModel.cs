using Resources.Models.Base;

namespace Resources.Models
{
    public class MainPageModel : BaseModel
    {
        public string PresetName { get; set; } = default!;
        public string Action { get; set; } = default!;
        public string Button { get; set; } = default!;
        public string Image { get; set; } = default!;
        public string Phrase { get; set; } = default!;
        public string Text { get; set; } = default!;
    }
}
