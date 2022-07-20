using Resources.Entities;
using Resources.Models;

namespace Services.Converters
{
    public static class ContactConverter
    {
        public static ContactModel ContactEntityToModel(this ContactEntity entity)
        {
            return new ContactModel
            {
                Id = entity.Id,
                Address = entity.Address,
                Postcode = entity.Postcode,
                Phone = entity.Phone,
                Fax = entity.Fax,
                MapAsString = DataConverter.Array64ToDataImageString(entity.MapAsArray64),
            };
        }

        public static ContactEntity ContactModelToEntity(this ContactModel model)
        {
            return new ContactEntity
            {
                Id = model.Id,
                Address = model.Address,
                Postcode = model.Postcode,
                Phone = model.Phone,
                Fax = model.Fax,
                MapAsArray64 = DataConverter.PathToImageToArray64(model.MapAsString),
                IsPublishedOnMainPage = false
            };
        }
    }
}
