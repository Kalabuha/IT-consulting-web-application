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

            var presetInfos = presets
                .Select(preset => new PresetInfo
                {
                    Id = preset.Id,
                    PresetName = preset.PresetName,
                    IsPublished = preset.IsPublishedOnMainPage
                })
                .ToList();

            return presetInfos;
        }

        public async Task<PresetInfo?> GetPublishedPresetInfoAsync()
        {
            var publishedPreset = await _presetRepository.GetPublishedMainPagePresetEntityAsync();
            if (publishedPreset == null)
            {
                return null;
            }

            var publishedPresetInfo = new PresetInfo
            {
                Id = publishedPreset.Id,
                PresetName = publishedPreset.PresetName,
                IsPublished = publishedPreset.IsPublishedOnMainPage
            };

            return publishedPresetInfo;
        }

        public async Task<PresetInfo?> GetPresetInfoByIdAsync(int presetId)
        {
            var preset = await _presetRepository.GetEntity(presetId);
            if (preset == null)
            {
                return null;
            }

            var presetInfo = new PresetInfo
            {
                Id = preset.Id,
                PresetName = preset.PresetName,
                IsPublished = preset.IsPublishedOnMainPage
            };

            return presetInfo;
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
            var publishedPreset =
                await _presetRepository.GetPublishedMainPagePresetEntityAsync() ?? CreateDefaultMainPagePresetEntity();

            var publishedMainPageModel = await AssembleModelFromPreset(publishedPreset);

            return publishedMainPageModel;
        }

        public async Task<MainPageModel> GetMainPageModelByIdAsync(int presetId)
        {
            var preset = await _presetRepository.GetEntity(presetId) ?? CreateDefaultMainPagePresetEntity();

            var mainPageModel = await AssembleModelFromPreset(preset);

            return mainPageModel;
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

        private async Task<MainPageModel> AssembleModelFromPreset(MainPagePresetEntity preset)
        {
            if (preset.Id <= 0)
            {
                return await CreateDefaultMainPageModel(preset);
            }

            var actionEntity = await _actionRepository.GetEntity(preset.ActionId);
            var buttonEntity = await _buttonRepository.GetEntity(preset.ButtonId);
            var imageEntity = await _imageRepository.GetEntity(preset.ImageId);
            var phraseEntity = await _phraseRepository.GetEntity(preset.PhraseId);
            var textEntity = await _textRepository.GetEntity(preset.TextId);

            var mainPageModel = new MainPageModel()
            {
                Id = preset.Id,
                PresetName = preset.PresetName,
                IsPublished = preset.IsPublishedOnMainPage,

                ActionId = preset.ActionId,
                Action = actionEntity != null ? actionEntity.Action : string.Empty,

                ButtonId = preset.ButtonId,
                Button = buttonEntity != null ? buttonEntity.Button : string.Empty,

                ImageId = preset.ImageId,
                Image = imageEntity != null ? DataConverter.Array64ToString(imageEntity.ImageAsArray64) : string.Empty,

                PhraseId = preset.PhraseId,
                Phrase = phraseEntity != null ? phraseEntity.Phrase : string.Empty,

                TextId = preset.TextId,
                Text = textEntity != null ? textEntity.Text : string.Empty
            };

            return mainPageModel;
        }

        private MainPagePresetEntity CreateDefaultMainPagePresetEntity()
        {
            var defaultMainPagePresetEntity = new MainPagePresetEntity()
            {
                Id = -1,
                PresetName = "Пресет по умолчанию",
            };

            return defaultMainPagePresetEntity;
        }

        private async Task<MainPageModel> CreateDefaultMainPageModel(MainPagePresetEntity defaultPreset)
        {
            var defaultMainPageModel = new MainPageModel()
            {
                Id = defaultPreset.Id,
                PresetName = defaultPreset.PresetName,
                IsPublished = false,

                Action = await GetDefaultTextFromFile("action.txt"),
                Button = string.Empty,
                Image = string.Empty,
                Phrase = string.Empty,
                Text = await GetDefaultTextFromFile("text.txt")
            };

            return defaultMainPageModel;
        }
    }
}
