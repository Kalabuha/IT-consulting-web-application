using Repositories.Interfaces;
using Resources.Models;
using Resources.Entities;
using Services.Converters;
using Services.Interfaces;
using Services.Common;

namespace Services
{
    internal class MainPageService : DefaultDataService, IMainPageService
    {
        private readonly IMainPagePresetRepository _presetRepository;

        private readonly IMainPageActionRepository _actionRepository;
        private readonly IMainPageButtonRepository _buttonRepository;
        private readonly IMainPageImageRepository _imageRepository;
        private readonly IMainPagePhraseRepository _phraseRepository;
        private readonly IMainPageTextRepository _textRepository;

        public MainPageService(
            IMainPagePresetRepository presetRepository,
            IMainPageActionRepository actionRepository,
            IMainPageButtonRepository buttonRepository,
            IMainPageImageRepository imageRepository,
            IMainPagePhraseRepository phraseRepository,
            IMainPageTextRepository textRepository)
        {
            _presetRepository = presetRepository;

            _actionRepository = actionRepository;
            _buttonRepository = buttonRepository;
            _imageRepository = imageRepository;
            _phraseRepository = phraseRepository;
            _textRepository = textRepository;
        }

        public async Task<List<MainPageModel>> GetAllMainPageModelsAsync()
        {
            var presets = await _presetRepository.GetAllMainPagePresetEntitiesAsync();

            var mainPageModels = new List<MainPageModel>();
            foreach (var preset in presets)
            {
                mainPageModels.Add(await AssembleModelFromPreset(preset));
            }

            return mainPageModels;
        }

        public async Task<MainPageModel> GetPublishedMainPageModelAsync()
        {
            var postedMainPagePreset = (await _presetRepository.GetAllMainPagePresetEntitiesAsync())
                .FirstOrDefault(p => p.IsPostedOnThePage == true);

            return await AssembleModelFromPreset(postedMainPagePreset);
        }

        private async Task<MainPageModel> AssembleModelFromPreset(MainPagePresetEntity? preset)
        {
            preset ??= new MainPagePresetEntity();

            var actionEntity = await _actionRepository.GetEntity(preset.ActionId);
            var buttonEntity = await _buttonRepository.GetEntity(preset.ButtonId);
            var imageEntity = await _imageRepository.GetEntity(preset.ImageId);
            var phraseEntity = await _phraseRepository.GetEntity(preset.PhraseId);
            var textEntity = await _textRepository.GetEntity(preset.TextId);

            var mainPageModel = new MainPageModel();

            mainPageModel.Id = preset.Id;
            mainPageModel.Button = buttonEntity != null ? buttonEntity.Button : await GetDefaultTextFromFile("button.txt");
            mainPageModel.Action = actionEntity != null ? actionEntity.Action : await GetDefaultTextFromFile("action.txt");
            mainPageModel.Image = imageEntity != null ? DataConverter.Array64ToString(imageEntity.ImageAsArray64) : GetDefaultImage("consulting.jpg");
            mainPageModel.Phrase = phraseEntity != null ? phraseEntity.Phrase : await GetDefaultTextFromFile("phrase.txt");
            mainPageModel.Text = textEntity != null ? textEntity.Text : await GetDefaultTextFromFile("text.txt");

            return mainPageModel;
        }
    }
}
