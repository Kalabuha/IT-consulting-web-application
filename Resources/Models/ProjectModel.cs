using Microsoft.AspNetCore.Http;
using Resources.Models.Base;

namespace Resources.Models
{
    public class ProjectModel : BaseModel
    {
        public string ProjectTitle { get; set; } = default!;
        public string CustomerCompanyLogoAsString { get; set; } = default!;
        public IFormFile? CustomerCompanyLogoAsFormFile { get; set; }
        public string LinkToCustomerSite { get; set; } = default!;
        public string ProjectDescription { get; set; } = default!;
        public bool IsPublished { get; set; }

        public string? ProjectTitleValidationMessage { get; set; }
        public string? ProjectDescriptionValidationMessage { get; set; }
    }
}
