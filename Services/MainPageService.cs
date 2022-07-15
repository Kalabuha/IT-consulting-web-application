using Repositories.Interfaces;
using Services.Converters;
using Services.Interfaces;
using Services.Common;
using Resources.Entities;
using Resources.Models;
using Resources.Models.Base;
using Resources.Containers;

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

        public async Task<MainPageModel?> GetPublishedMainPageModelAsync()
        {
            var publishedPreset = await _presetRepository.GetPublishedMainPagePresetEntityAsync();
            if (publishedPreset == null)
            {
                return null;
            }

            var publishedMainPageModel = await AssembleModelFromPreset(publishedPreset);

            return publishedMainPageModel;
        }

        public async Task<MainPageModel?> GetMainPageModelByIdAsync(int presetId)
        {
            var preset = await _presetRepository.GetEntity(presetId);
            if (preset == null)
            {
                return null;
            }

            var mainPageModel = await AssembleModelFromPreset(preset);

            return mainPageModel;
        }

        public async Task<MainPageModel> GetDefaultMainPageModelAsync()
        {
            var defaultPreset = CreateDefaultMainPagePresetEntity();

            var defaultMainPageModel = await CreateDefaultMainPageModel(defaultPreset);

            return defaultMainPageModel;
        }

        public async Task<MainPageAllDataContainer> GetMainPageAllData()
        {
            var actions = await _actionRepository.GetAllMainPageActionEntitiesAsync();
            var buttons = await _buttonRepository.GetAllMainPageButtonEntitiesAsync();
            var images = await _imageRepository.GetAllMainPageImageEntitiesAsync();
            var phrases = await _phraseRepository.GetAllMainPagePhraseEntitiesAsync();
            var texts = await _textRepository.GetAllMainPageTextEntitiesAsync();

            return new MainPageAllDataContainer
            {
                Actions = actions.Select(a => a.ActionEntityToModel()).ToList(),
                Buttons = buttons.Select(b => b.ButtonEntityToModel()).ToList(),
                Images = images.Select(i => i.ImageEntityToModel()).ToList(),
                Phrases = phrases.Select(p => p.PhraseEntityToModel()).ToList(),
                Texts = texts.Select(t => t.TextEntityToModel()).ToList()
            };
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

        public async Task DeletePreset(int presetId)
        {
            var deletedPreset = await _presetRepository.GetEntity(presetId);
            if (deletedPreset != null)
            {
                await _presetRepository.RemoveEntityAsync(deletedPreset);
            }
        }

        public async Task<int> CreatePreset(string presetName)
        {
            presetName = DataConverter.CutTextByParameter(presetName, 40);

            var newPresetEntity = new MainPagePresetEntity()
            {
                PresetName = presetName,
            };

            await _presetRepository.AddEntityAsync(newPresetEntity);
            return newPresetEntity.Id;
        }

        public async Task ChangePresetElement(int presetId, int elementId, Type elementModelType)
        {
            var preset = await _presetRepository.GetEntity(presetId);
            if (preset == null) return;

            if (elementModelType == typeof(MainPageActionModel))
            {
                if (elementId > 0)
                {
                    var action = await _actionRepository.GetEntity(elementId);
                    preset.Action = action;
                }
                else preset.ActionId = null;
            }
            else if (elementModelType == typeof(MainPageButtonModel))
            {
                if (elementId > 0)
                {
                    var button = await _buttonRepository.GetEntity(elementId);
                    preset.Button = button;
                }
                else preset.ButtonId = null;
            }
            else if (elementModelType == typeof(MainPageImageModel))
            {
                if (elementId > 0)
                {
                    var image = await _imageRepository.GetEntity(elementId);
                    preset.Image = image;
                }
                else preset.ImageId = null;
            }
            else if (elementModelType == typeof(MainPagePhraseModel))
            {
                if (elementId > 0)
                {
                    var phrase = await _phraseRepository.GetEntity(elementId);
                    preset.Phrase = phrase;
                }
                else preset.PhraseId = null;
            }
            else if (elementModelType == typeof(MainPageTextModel))
            {
                if (elementId > 0)
                {
                    var text = await _textRepository.GetEntity(elementId);
                    preset.Text = text;
                }
                else preset.TextId = null;
            }
            else return;

            await _presetRepository.UpdateEntityAsync(preset);
        }

        public async Task TryDeletePresetElement(int elementId, Type elementModelType)
        {
            try
            {
                if (elementModelType == typeof(MainPageActionModel))
                {
                    var action = await _actionRepository.GetEntity(elementId);
                    if (action != null)
                        await _actionRepository.RemoveEntityAsync(action);
                }
                else if (elementModelType == typeof(MainPageButtonModel))
                {
                    var button = await _buttonRepository.GetEntity(elementId);
                    if (button != null)
                        await _buttonRepository.RemoveEntityAsync(button);
                }
                else if (elementModelType == typeof(MainPageImageModel))
                {
                    var image = await _imageRepository.GetEntity(elementId);
                    if (image != null)
                        await _imageRepository.RemoveEntityAsync(image);
                }
                else if (elementModelType == typeof(MainPagePhraseModel))
                {
                    var phrase = await _phraseRepository.GetEntity(elementId);
                    if (phrase != null)
                        await _phraseRepository.RemoveEntityAsync(phrase);
                }
                else if (elementModelType == typeof(MainPageTextModel))
                {
                    var text = await _textRepository.GetEntity(elementId);
                    if (text != null)
                        await _textRepository.RemoveEntityAsync(text);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private async Task<MainPageModel> AssembleModelFromPreset(MainPagePresetEntity preset)
        {
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
                Action = actionEntity?.Action ?? string.Empty,

                ButtonId = preset.ButtonId,
                Button = buttonEntity?.Button ?? string.Empty,

                ImageId = preset.ImageId,
                Image = imageEntity != null ? DataConverter.Array64ToString(imageEntity.ImageAsArray64) : string.Empty,

                PhraseId = preset.PhraseId,
                Phrase = phraseEntity?.Phrase ?? string.Empty,

                TextId = preset.TextId,
                Text = textEntity?.Text ?? string.Empty
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
