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

        public async Task<List<PresetInfo>> GetAllPresetInfosAsync()
        {
            var presets = await _presetRepository.GetAllMainPagePresetEntitiesAsync();

            var presetInfos = new List<PresetInfo>();
            foreach (var preset in presets)
            {
                presetInfos.Add(new PresetInfo
                {
                    Id = preset.Id,
                    PresetName = preset.PresetName,
                    IsPublished = preset.IsPublishedOnMainPage
                });
            }

            return presetInfos;
        }

        public async Task<PresetInfo?> GetPublishedPresetInfoAsync()
        {
            var publishedPreset = await _presetRepository.GetPublishedMainPagePresetEntityAsync();
            if (publishedPreset == null)
            {
                return null;
            }

            return new PresetInfo
            {
                Id = publishedPreset.Id,
                PresetName = publishedPreset.PresetName,
                IsPublished = publishedPreset.IsPublishedOnMainPage
            };
        }

        public async Task<PresetInfo?> GetPresetInfoByIdAsync(int presetId)
        {
            var preset = await _presetRepository.GetEntity(presetId);
            if (preset == null)
            {
                return null;
            }

            return new PresetInfo
            {
                Id = preset.Id,
                PresetName = preset.PresetName,
                IsPublished = preset.IsPublishedOnMainPage
            };
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
            var publishedMainPagePreset = await _presetRepository.GetPublishedMainPagePresetEntityAsync();
            var publishedMainPageModel = await AssembleModelFromPreset(publishedMainPagePreset);

            return publishedMainPageModel;
        }

        public async Task<MainPageModel> GetMainPageModelByIdAsync(int presetId)
        {
            var preset = await _presetRepository.GetEntity(presetId);

            return await AssembleModelFromPreset(preset);
        }

        public async Task PublishPreset(int presetId)
        {
            var publishPreset = await _presetRepository.GetEntity(presetId);
            if (publishPreset == null)
            {
                return;
            }

            var presets = await _presetRepository.GetAllMainPagePresetEntitiesAsync();
            foreach (var preset in presets)
            {
                if (preset.IsPublishedOnMainPage)
                {
                    preset.IsPublishedOnMainPage = false;
                    await _presetRepository.UpdateEntityAsync(preset);
                }
            }

            publishPreset.IsPublishedOnMainPage = true;
            await _presetRepository.UpdateEntityAsync(publishPreset);
        }

        private async Task<MainPageModel> AssembleModelFromPreset(MainPagePresetEntity? preset)
        {
            preset ??= new MainPagePresetEntity()
            {
                Id = -1,
                PresetName = "Пресет по умолчанию"
            };

            var actionEntity = await _actionRepository.GetEntity(preset.ActionId);
            var buttonEntity = await _buttonRepository.GetEntity(preset.ButtonId);
            var imageEntity = await _imageRepository.GetEntity(preset.ImageId);
            var phraseEntity = await _phraseRepository.GetEntity(preset.PhraseId);
            var textEntity = await _textRepository.GetEntity(preset.TextId);

            var mainPageModel = new MainPageModel();

            mainPageModel.Id = preset.Id;
            mainPageModel.PresetName = preset.PresetName;
            mainPageModel.Button = buttonEntity != null ? buttonEntity.Button : await GetDefaultTextFromFile("button.txt");
            mainPageModel.Action = actionEntity != null ? actionEntity.Action : await GetDefaultTextFromFile("action.txt");
            mainPageModel.Image = imageEntity != null ? DataConverter.Array64ToString(imageEntity.ImageAsArray64) : GetDefaultImage("consulting.jpg");
            mainPageModel.Phrase = phraseEntity != null ? phraseEntity.Phrase : await GetDefaultTextFromFile("phrase.txt");
            mainPageModel.Text = textEntity != null ? textEntity.Text : await GetDefaultTextFromFile("text.txt");

            return mainPageModel;
        }
    }
}
