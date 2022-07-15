﻿using Resources.Models;

namespace WebAppForAdmins.Models
{
    public class MainViewModel
    {
        public MainPageModel MainPageModel { get; set; } = default!;
        public IList<PresetInfo> PresetInfos { get; set; } = default!;

        public IList<MainPageActionModel> Actions { get; set; } = default!;
        public IList<MainPageButtonModel> Buttons { get; set; } = default!;
        public IList<MainPageImageModel> Images { get; set; } = default!;
        public IList<MainPagePhraseModel> Phrases { get; set; } = default!;
        public IList<MainPageTextModel> Texts { get; set; } = default!;
    }
}
