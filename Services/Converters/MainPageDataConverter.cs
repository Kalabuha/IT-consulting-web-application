using Resources.Entities;
using Resources.Models;

namespace Services.Converters
{
    public static class MainPageDataConverter
    {
        public static MainPageActionModel ActionEntityToModel(this MainPageActionEntity entity)
        {
            return new MainPageActionModel
            {
                Id = entity.Id,
                Action = entity.Action,
            };
        }

        public static MainPageActionEntity ActionModelToEntity(this MainPageActionModel model)
        {
            return new MainPageActionEntity
            {
                Id = model.Id,
                Action = DataConverter.CutTextByParameter(model.Action, 60),
            };
        }

        public static MainPageButtonModel ButtonEntityToModel(this MainPageButtonEntity entity)
        {
            return new MainPageButtonModel
            {
                Id = entity.Id,
                Button = entity.Button,
            };
        }

        public static MainPageButtonEntity ButtonModelToEntity(this MainPageButtonModel model)
        {
            return new MainPageButtonEntity
            {
                Id = model.Id,
                Button = DataConverter.CutTextByParameter(model.Button, 16),
            };
        }

        public static MainPageImageModel ImageEntityToModel(this MainPageImageEntity entity)
        {
            return new MainPageImageModel
            {
                Id = entity.Id,
                Image = DataConverter.Array64ToString(entity.ImageAsArray64),
            };
        }

        public static MainPageImageEntity ImageModelToEntity(this MainPageImageModel model)
        {
            return new MainPageImageEntity
            {
                Id = model.Id,
                ImageAsArray64 = Convert.FromBase64String(model.Image),
            };
        }

        public static MainPagePhraseModel PhraseEntityToModel(this MainPagePhraseEntity entity)
        {
            return new MainPagePhraseModel
            {
                Id = entity.Id,
                Phrase = entity.Phrase,
            };
        }

        public static MainPagePhraseEntity PhraseModelToEntity(this MainPagePhraseModel model)
        {
            return new MainPagePhraseEntity
            {
                Id = model.Id,
                Phrase = DataConverter.CutTextByParameter(model.Phrase, 44),
            };
        }

        public static MainPageTextModel TextEntityToModel(this MainPageTextEntity entity)
        {
            return new MainPageTextModel
            {
                Id = entity.Id,
                Text = entity.Text,
            };
        }

        public static MainPageTextEntity TextModelToEntity(this MainPageTextModel model)
        {
            return new MainPageTextEntity
            {
                Id = model.Id,
                Text = DataConverter.CutTextByParameter(model.Text, 4000),
            };
        }
    }
}
