using Resources.Entities;
using Resources.Models;

namespace Services.Converters
{
    public static class BlogConverter
    {
        public static BlogModel BlogEntityToModel(this BlogEntity entity)
        {
            return new BlogModel
            {
                Id = entity.Id,
                BlogTitle = entity.Title,
                ShortBlogDescription = entity.ShortBlogDescription,
                LongBlogDescription = entity.LongBlogDescription,
                PublicationDate = entity.PublicationDate,
                BlogImageAsString = DataConverter.Array64ToString(entity.BlogImageAsArray64)
            };
        }

        public static BlogEntity BlogModelToEntity(this BlogModel model)
        {
            return new BlogEntity
            {
                Id = model.Id,
                Title = DataConverter.CutTextByParameter(model.BlogTitle, 150),
                ShortBlogDescription = DataConverter.CutTextByParameter(model.ShortBlogDescription, 500),
                LongBlogDescription = DataConverter.CutTextByParameter(model.LongBlogDescription, 6000),
                PublicationDate = model.PublicationDate,
                BlogImageAsArray64 = DataConverter.PathToImageToArray64(model.BlogImageAsString),
                IsPublished = true,
            };
        }
    }
}
