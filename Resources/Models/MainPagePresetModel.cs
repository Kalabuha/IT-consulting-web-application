using Resources.Models.Base;

namespace Resources.Models
{
    public class MainPagePresetModel : BaseModel
    {
        public string PresetName { get; set; } = default!;
        public bool IsPublished { get; set; }

        public int? ActionId { get; set; }
        public int? ButtonId { get; set; }
        public int? ImageId { get; set; }
        public int? PhraseId { get; set; }
        public int? TextId { get; set; }
    }
}
