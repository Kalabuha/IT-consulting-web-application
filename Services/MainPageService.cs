using Repositories.Interfaces;
using Resources.Entities;
using Resources.Models;
using Resources.Models.Base;
using Services.Common;
using Services.Converters;
using Services.Interfaces;

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

        public async Task<List<MainPagePresetModel>> GetAllPresetModelsAsync()
        {
            var presetEntities = await _presetRepository.GetAllMainPagePresetEntitiesAsync();

            var presetModels = presetEntities
                .Select(p => p.MainPagePresetEntityToModel())
                .ToList();

            return presetModels;
        }

        public async Task<MainPagePresetModel?> GetPublishedPresetModelAsync()
        {
            var publishedPresetEntity = await _presetRepository.GetPublishedMainPagePresetEntityAsync();

            var publishedPresetModel = publishedPresetEntity?.MainPagePresetEntityToModel();

            return publishedPresetModel;
        }

        public async Task<MainPagePresetModel?> GetPresetModelByIdAsync(int id)
        {
            var presetEntity = await _presetRepository.GetEntity(id);

            var presetModel = presetEntity?.MainPagePresetEntityToModel();

            return presetModel;
        }

        public MainPagePresetModel GetDefaultPresetModelAsync()
        {
            var defaultPresetModel = new MainPagePresetModel
            {
                Id = -1,
                PresetName = "Пресет по умолчанию",

                ActionId = -1,
                ButtonId = -1,
                ImageId = -1,
                PhraseId = -1,
                TextId = -1
            };

            return defaultPresetModel;
        }

        public async Task PublishPreset(int id)
        {
            var entity = await _presetRepository.GetEntity(id);
            if (entity == null)
            {
                return;
            }

            var publishEntities = await _presetRepository.GetAllPublishedPresetEntityAsync();
            foreach (var publishEntity in publishEntities)
            {
                publishEntity.IsPublishedOnMainPage = false;
                await _presetRepository.UpdateEntityAsync(publishEntity);
            }

            entity.IsPublishedOnMainPage = true;
            await _presetRepository.UpdateEntityAsync(entity);
        }

        public async Task<int> CreatePreset(MainPagePresetModel model)
        {
            var entity = new MainPagePresetEntity()
            {
                PresetName = DataConverter.CutTextByParameterIfNullReturnEmpty(model.PresetName, 40),
            };

            await _presetRepository.AddEntityAsync(entity);
            return entity.Id;
        }

        public async Task UpdatePreset(MainPagePresetModel model)
        {
            var entity = await _presetRepository.GetEntity(model.Id);
            if (entity == null)
            {
                return;
            }

            entity.PresetName = model.PresetName;
            entity.IsPublishedOnMainPage = model.IsPublished;
            entity.ActionId = model.ActionId;
            entity.ButtonId = model.ButtonId;
            entity.ImageId = model.ImageId;
            entity.PhraseId = model.PhraseId;
            entity.TextId = model.TextId;

            await _presetRepository.UpdateEntityAsync(entity);
        }

        public async Task DeletePreset(MainPagePresetModel model)
        {
            var entity = await _presetRepository.GetEntity(model.Id);
            if (entity == null)
            {
                return;
            }

            await _presetRepository.RemoveEntityAsync(entity);
        }

        public async Task<MainPageTModel?> GetElementModelByIdAsync<MainPageTModel>(int id) where MainPageTModel : BaseModel
        {
            BaseModel? elementModel;
            if (typeof(MainPageTModel) == typeof(MainPageActionModel))
            {
                var actionEntity = await _actionRepository.GetEntity(id);
                elementModel = actionEntity?.ActionEntityToModel();
            }
            else if (typeof(MainPageTModel) == typeof(MainPageButtonModel))
            {
                var buttonEntity = await _buttonRepository.GetEntity(id);
                elementModel = buttonEntity?.ButtonEntityToModel();
            }
            else if (typeof(MainPageTModel) == typeof(MainPageImageModel))
            {
                var imageEntity = await _imageRepository.GetEntity(id);
                elementModel = imageEntity?.ImageEntityToModel();
            }
            else if (typeof(MainPageTModel) == typeof(MainPagePhraseModel))
            {
                var phraseEntity = await _phraseRepository.GetEntity(id);
                elementModel = phraseEntity?.PhraseEntityToModel();
            }
            else if (typeof(MainPageTModel) == typeof(MainPageTextModel))
            {
                var textEntity = await _textRepository.GetEntity(id);
                elementModel = textEntity?.TextEntityToModel();
            }
            else
            {
                throw new ApplicationException("Неизвестная модель");
            }

            return (MainPageTModel?)elementModel;
        }

        public async Task<MainPageTModel?> GetElementModelByPresetIdAsync<MainPageTModel>(int id) where MainPageTModel : BaseModel
        {
            var preset = await _presetRepository.GetEntity(id);
            if (preset == null)
            {
                return null;
            }

            BaseModel? elementModel;
            if (typeof(MainPageTModel) == typeof(MainPageActionModel))
            {
                var actionEntity = await _actionRepository.GetEntity(preset.ActionId);
                elementModel = actionEntity?.ActionEntityToModel();
            }
            else if (typeof(MainPageTModel) == typeof(MainPageButtonModel))
            {
                var buttonEntity = await _buttonRepository.GetEntity(preset.ButtonId);
                elementModel = buttonEntity?.ButtonEntityToModel();
            }
            else if (typeof(MainPageTModel) == typeof(MainPageImageModel))
            {
                var imageEntity = await _imageRepository.GetEntity(preset.ImageId);
                elementModel = imageEntity?.ImageEntityToModel();
            }
            else if (typeof(MainPageTModel) == typeof(MainPagePhraseModel))
            {
                var phraseEntity = await _phraseRepository.GetEntity(preset.PhraseId);
                elementModel = phraseEntity?.PhraseEntityToModel();
            }
            else if (typeof(MainPageTModel) == typeof(MainPageTextModel))
            {
                var textEntity = await _textRepository.GetEntity(preset.TextId);
                elementModel = textEntity?.TextEntityToModel();
            }
            else
            {
                throw new ApplicationException("Неизвестная модель");
            }

            return (MainPageTModel?)elementModel;
        }

        public async Task<List<MainPageTModel>> GetAllElementModelsAsync<MainPageTModel>() where MainPageTModel : BaseModel
        {
            IEnumerable<BaseModel> elementModels;
            if (typeof(MainPageTModel) == typeof(MainPageActionModel))
            {
                var actionEntities = await _actionRepository.GetAllMainPageActionEntitiesAsync();
                elementModels = actionEntities.Select(a => a.ActionEntityToModel());
            }
            else if (typeof(MainPageTModel) == typeof(MainPageButtonModel))
            {
                var buttonEntities = await _buttonRepository.GetAllMainPageButtonEntitiesAsync();
                elementModels = buttonEntities.Select(b => b.ButtonEntityToModel());
            }
            else if (typeof(MainPageTModel) == typeof(MainPageImageModel))
            {
                var imageEntities = await _imageRepository.GetAllMainPageImageEntitiesAsync();
                elementModels = imageEntities.Select(i => i.ImageEntityToModel());
            }
            else if (typeof(MainPageTModel) == typeof(MainPagePhraseModel))
            {
                var phraseEntities = await _phraseRepository.GetAllMainPagePhraseEntitiesAsync();
                elementModels = phraseEntities.Select(p => p.PhraseEntityToModel());
            }
            else if (typeof(MainPageTModel) == typeof(MainPageTextModel))
            {
                var phraseEntities = await _textRepository.GetAllMainPageTextEntitiesAsync();
                elementModels = phraseEntities.Select(p => p.TextEntityToModel());
            }
            else
            {
                throw new ApplicationException("Неизвестная модель");
            }

            return elementModels.Select(e => (MainPageTModel)e).ToList();
        }

        public async Task<MainPageTextModel> GetDefaultMainPageTextModel()
        {
            var defaultMainPageTextModel = new MainPageTextModel
            {
                Id = -1,
                Text = await GetDefaultTextFromFileAsync("text.txt")
            };

            return defaultMainPageTextModel;
        }

        public async Task<MainPageActionModel> GetDefaultMainPageActionModel()
        {
            var defaultMainPageActionModel = new MainPageActionModel
            {
                Id = -1,
                Action = await GetDefaultTextFromFileAsync("action.txt")
            };

            return defaultMainPageActionModel;
        }
    }
}
