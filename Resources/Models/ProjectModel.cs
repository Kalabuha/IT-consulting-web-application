using Resources.Models.Base;

namespace Resources.Models
{
    public class ProjectModel : BaseModel
    {
        public string ProjectTitle { get; set; } = default!;
        public string CustomerCompanyLogoAsString { get; set; } = default!;
        public string LinkToCustomerSite { get; set; } = default!;
        public string ProjectDescription { get; set; } = default!;
    }
}
