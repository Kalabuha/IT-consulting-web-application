using Resources.Models.Base;

namespace Resources.Models
{
    public class MainPageModel : BaseModel
    {
        public string PresetName { get; set; } = default!;
        public bool IsPublished { get; set; }

        public int? ActionId { get; set; }
        public string Action { get; set; } = default!;

        public int? ButtonId { get; set; }
        public string Button { get; set; } = default!;

        public int? ImageId { get; set; }
        public string Image { get; set; } = default!;

        public int? PhraseId { get; set; }
        public string Phrase { get; set; } = default!;

        public int? TextId { get; set; }
        public string Text { get; set; } = default!;
    }
}
