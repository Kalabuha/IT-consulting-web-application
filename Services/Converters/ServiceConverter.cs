using Resources.Entities;
using Resources.Models;


namespace Services.Converters
{
    public static class ServiceConverter
    {
        public static ServiceModel ServiceEntityToModel(this ServiceEntity entity)
        {
            return new ServiceModel
            {
                Id = entity.Id,
                ServiceName = entity.Title,
                ServiceDescription = entity.ServiceDescription
            };
        }

        public static ServiceEntity ServiceModelToEntity(this ServiceModel model)
        {
            return new ServiceEntity
            {
                Id = model.Id,
                Title = DataConverter.CutTextByParameter(model.ServiceName, 150),
                ServiceDescription = DataConverter.CutTextByParameter(model.ServiceDescription, 1500),
                IsPublished = true
            };
        }
    }
}
