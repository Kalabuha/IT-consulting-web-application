using Services.Interfaces;
using Services.Enums;
using Services.ServiceResources;
using Resources.Models;

namespace Services
{
    internal class ProjectDataValidationService : IProjectDataValidationService
    {
        const string UnknownTestResult = "Результат проверки не известен";

        public ProjectValidationResult GetProjectValidationResult(ProjectModel project)
        {
            var titleValidationCheckResult = TitleValidationCheck(project.ProjectTitle);
            var descriptionValidationCheckResult = DescriptionValidationCheck(project.ProjectDescription);

            return new ProjectValidationResult
            {
                TitleValidError = GetErrorMessageInTitle(titleValidationCheckResult),
                DescriptionValidError = GetErrorMessageInDescription(descriptionValidationCheckResult),
            };
        }

        private string? GetErrorMessageInTitle(ProjectDataValidationResult titleValidationCheckResult)
        {
            switch (titleValidationCheckResult)
            {
                case ProjectDataValidationResult.TitleIsEmpty:
                    return "Поле с заголовком не заполнено";

                case ProjectDataValidationResult.TitlePassedValidationCheck:
                    return null;

                default:
                    throw new ApplicationException(UnknownTestResult);
            }
        }

        private string? GetErrorMessageInDescription(ProjectDataValidationResult descriptionValidationCheckResult)
        {
            switch (descriptionValidationCheckResult)
            {
                case ProjectDataValidationResult.DescriptionIsEmpty:
                    return "Поле с описанием не заполнено";

                case ProjectDataValidationResult.DescriptionPassedValidationCheck:
                    return null;

                default:
                    throw new ApplicationException(UnknownTestResult);
            }
        }

        private ProjectDataValidationResult TitleValidationCheck(string? title)
        {
            if (IsNullOrEmptyOrWhitespace(title))
            {
                return ProjectDataValidationResult.TitleIsEmpty;
            }

            return ProjectDataValidationResult.TitlePassedValidationCheck;
        }

        private ProjectDataValidationResult DescriptionValidationCheck(string? description)
        {
            if (IsNullOrEmptyOrWhitespace(description))
            {
                return ProjectDataValidationResult.DescriptionIsEmpty;
            }

            return ProjectDataValidationResult.DescriptionPassedValidationCheck;
        }

        private bool IsNullOrEmptyOrWhitespace(string? input)
        {
            return string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input);
        }
    }
}
