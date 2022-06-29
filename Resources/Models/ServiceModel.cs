using Resources.Models.Base;

namespace Resources.Models
{
    public class ServiceModel : BaseModel
    {
        public string ServiceName { get; set; } = default!;
        public string ServiceDescription { get; set; } = default!;
    }
}
