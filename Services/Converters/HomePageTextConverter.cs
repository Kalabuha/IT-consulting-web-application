using Resources.Entities;
using Resources.Models;

namespace Services.Converters
{
    public static class HomePageTextConverter
    {
        public static TextModel TextEntityToModel(this HomePageTextEntity entity)
        {
            return new TextModel
            {
                Id = entity.Id,
                Text = entity.Text,
            };
        }

        private static HomePageTextEntity TextModelToEntity(this TextModel model)
        {
            return new HomePageTextEntity
            {
                Id = model.Id,
                Text = DataConverter.CutTextByParameter(model.Text, 4000),
                IsPostedOnThePage = false
            };
        }
    }
}
