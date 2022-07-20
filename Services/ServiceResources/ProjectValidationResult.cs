namespace Services.ServiceResources
{
    public class ProjectValidationResult
    {
        public bool IsDataNotVerified { get; set; } = false;

        private string? titleValidError;
        public string? TitleValidError
        {
            get { return titleValidError; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    IsDataNotVerified = true;
                    titleValidError = value;
                }
            }
        }

        private string? descriptionValidError;
        public string? DescriptionValidError
        {
            get { return descriptionValidError; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    IsDataNotVerified = true;
                    descriptionValidError = value;
                }
            }
        }
    }
}
