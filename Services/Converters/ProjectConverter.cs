using Resources.Entities;
using Resources.Models;

namespace Services.Converters
{
    public static class ProjectConverter
    {
        public static ProjectModel ProjectEntityToModel(this ProjectEntity entity)
        {
            return new ProjectModel
            {
                Id = entity.Id,
                ProjectTitle = entity.Title,
                LinkToCustomerSite = entity.LinkToCustomerSite ?? string.Empty,
                ProjectDescription = entity.ProjectDescription,
                CustomerCompanyLogoAsString = DataConverter.Array64ToString(entity.CustomerCompanyLogoAsArray64),
            };
        }

        public static ProjectEntity ProjectModelToEntity(this ProjectModel model)
        {
            return new ProjectEntity
            {
                Id = model.Id,
                Title = DataConverter.CutTextByParameter(model.ProjectTitle, 150),
                LinkToCustomerSite = DataConverter.CutTextByParameter(model.LinkToCustomerSite, 300),
                ProjectDescription = DataConverter.CutTextByParameter(model.ProjectDescription, 5000),
                CustomerCompanyLogoAsArray64 = DataConverter.PathToImageToArray64(model.CustomerCompanyLogoAsString),
                IsPublished = true
            };
        }
    }
}
