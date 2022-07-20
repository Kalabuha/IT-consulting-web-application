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
                CustomerCompanyLogoAsString = DataConverter.Array64ToDataImageString(entity.CustomerCompanyLogoAsArray64),
                IsPublished = entity.IsPublished
            };
        }

        public static ProjectEntity ProjectModelToEntity(this ProjectModel model)
        {
            return new ProjectEntity
            {
                Id = model.Id,
                Title = model.ProjectTitle,
                LinkToCustomerSite = string.IsNullOrEmpty(model.LinkToCustomerSite) ? null : model.LinkToCustomerSite,
                ProjectDescription = model.ProjectDescription,
                CustomerCompanyLogoAsArray64 = DataConverter.DataImageStringToArray64(model.CustomerCompanyLogoAsString),
                IsPublished = model.IsPublished
            };
        }
    }
}
