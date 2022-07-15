using Resources.Models;

namespace Resources.Containers
{
    public class MainPageAllDataContainer
    {
        public IList<MainPageActionModel> Actions { get; set; } = default!;
        public IList<MainPageButtonModel> Buttons { get; set; } = default!;
        public IList<MainPageImageModel> Images { get; set; } = default!;
        public IList<MainPagePhraseModel> Phrases { get; set; } = default!;
        public IList<MainPageTextModel> Texts { get; set; } = default!;
    }
}
