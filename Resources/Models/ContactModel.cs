using Resources.Models.Base;

namespace Resources.Models
{
    public class ContactModel : BaseModel
    {
        public int Postcode { get; set; }

        public string Address { get; set; } = default!;

        public string Phone { get; set; } = default!;

        public string? Fax { get; set; }

        public string MapAsString { get; set; } = default!;
    }
}
