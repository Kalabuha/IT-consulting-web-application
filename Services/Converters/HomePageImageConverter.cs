using Resources.Entities;
using Resources.Models;

namespace Services.Converters
{
    public static class HomePageImageConverter
    {
        public static ImageModel ImageEntityToModel(this HomePageImageEntity entity)
        {
            return new ImageModel
            {
                Id = entity.Id,
                ImageAsString = DataConverter.Array64ToString(entity.ImageAsArray64)
            };
        }

        private static HomePageImageEntity ImageModelToEntity(this ImageModel model)
        {
            return new HomePageImageEntity
            {
                Id = model.Id,
                ImageAsArray64 = DataConverter.PathToImageToArray64(model.ImageAsString),
                IsPostedOnThePage = false
            };
        }
    }
}
