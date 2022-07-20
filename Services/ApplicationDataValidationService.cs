using Services.Enums;
using Services.Interfaces;
using Services.ServiceResources;
using Resources.Models;

namespace Services
{
    internal class ApplicationDataValidationService : IApplicationDataValidationService
    {
        const int LowerNameLengthLimit = 3;
        const int UpperNameLengthLimit = 60;

        const int LowerEmailLengthLimit = 4;
        const int UpperEmailLengthLimit = 100;

        const int LowerMessageLengthLimit = 6;
        const int UpperMessageLengthLimit = 4000;

        const string UnknownTestResult = "Результат проверки не известен";

        public ApplicationValidationResult GetApplicationValidationResult(ApplicationModel application)
        {
            var nameValidationCheckResult = NameValidationCheck(application.GuestName);
            var emailValidationCheckResult = EmailValidationCheck(application.GuestEmail);
            var messageValidationCheckResult = MessageValidationCheck(application.GuestApplicationText);

            return new ApplicationValidationResult
            {
                NameValidError = GetErrorMessageInName(nameValidationCheckResult),
                EmailValidError = GetErrorMessageInEmail(emailValidationCheckResult),
                MessageValidError = GetErrorMessageInMessage(messageValidationCheckResult)
            };
        }

        private string? GetErrorMessageInName(ApplicationDataValidationResult nameValidationCheckResult)
        {
            switch (nameValidationCheckResult)
            {
                case ApplicationDataValidationResult.NameIsEmpty:
                    return "Поле с вашим именем не заполнено";
                case ApplicationDataValidationResult.NameIsTooShort:
                    return $"Имя должно быть не короче {LowerNameLengthLimit} символов";
                case ApplicationDataValidationResult.NameIsTooLong:
                    return $"Имя должно быть не длиннее {UpperNameLengthLimit} символов";
                case ApplicationDataValidationResult.NameContainsInvalidCharacters:
                    return "Имя должно содержать только буквенные символы";
                case ApplicationDataValidationResult.NamePassedValidationCheck:
                    return null;

                default:
                    throw new ApplicationException(UnknownTestResult);
            }
        }

        private string? GetErrorMessageInEmail(ApplicationDataValidationResult emailValidationCheckResult)
        {
            switch (emailValidationCheckResult)
            {
                case ApplicationDataValidationResult.EmailIsEmpty:
                    return "Поле с вашим e-mail не заполнено";
                case ApplicationDataValidationResult.EmailIsTooShort:
                    return $"Допускается не менее {LowerEmailLengthLimit} символов";
                case ApplicationDataValidationResult.EmailIsTooLong:
                    return $"Допускается не более {UpperEmailLengthLimit} символов";
                case ApplicationDataValidationResult.EmailInvalidFirstCharacter:
                    return "Не допускается вводить спец. символ в начале e-mail";
                case ApplicationDataValidationResult.EmailPassedValidationCheck:
                    return null;

                default:
                    throw new ApplicationException(UnknownTestResult);
            }
        }

        private string? GetErrorMessageInMessage(ApplicationDataValidationResult messageValidationCheckResult)
        {
            switch (messageValidationCheckResult)
            {
                case ApplicationDataValidationResult.MessageIsEmpty:
                    return "Поле с сообщением не заполнено";
                case ApplicationDataValidationResult.MessageIsTooShort:
                    return $"Вы ввели слишком короткое сообщение. Допускается не менее {LowerMessageLengthLimit} символов";
                case ApplicationDataValidationResult.MessageIsTooLong:
                    return $"Вы ввели слишком длинное сообщение. Допускается не более {UpperMessageLengthLimit} символов";
                case ApplicationDataValidationResult.MessagePassedValidationCheck:
                    return null;

                default:
                    throw new ApplicationException(UnknownTestResult);
            }
        }

        private ApplicationDataValidationResult NameValidationCheck(string? name)
        {
            if (IsNullOrEmptyOrWhitespace(name))
            {
                return ApplicationDataValidationResult.NameIsEmpty;
            }

            var nameLength = name!.Length;
            if (nameLength < LowerNameLengthLimit)
            {
                return ApplicationDataValidationResult.NameIsTooShort;
            }
            else if (nameLength > UpperNameLengthLimit)
            {
                return ApplicationDataValidationResult.NameIsTooLong;
            }

            if (!IsAllCharIsLetters(name))
            {
                return ApplicationDataValidationResult.NameContainsInvalidCharacters;
            }

            return ApplicationDataValidationResult.NamePassedValidationCheck;
        }

        private ApplicationDataValidationResult EmailValidationCheck(string? email)
        {
            if (IsNullOrEmptyOrWhitespace(email))
            {
                return ApplicationDataValidationResult.EmailIsEmpty;
            }

            var emailLength = email!.Length;
            if (emailLength < LowerEmailLengthLimit)
            {
                return ApplicationDataValidationResult.EmailIsTooShort;
            }
            else if (emailLength > UpperEmailLengthLimit)
            {
                return ApplicationDataValidationResult.EmailIsTooLong;
            }

            if (!IsFirstCharIsLetters(email))
            {
                return ApplicationDataValidationResult.EmailInvalidFirstCharacter;
            }

            return ApplicationDataValidationResult.EmailPassedValidationCheck;
        }

        private ApplicationDataValidationResult MessageValidationCheck(string? message)
        {
            if (IsNullOrEmptyOrWhitespace(message))
            {
                return ApplicationDataValidationResult.MessageIsEmpty;
            }

            var messageLength = message!.Length;
            if (messageLength < LowerMessageLengthLimit)
            {
                return ApplicationDataValidationResult.MessageIsTooShort;
            }
            else if (messageLength > UpperMessageLengthLimit)
            {
                return ApplicationDataValidationResult.MessageIsTooLong;
            }

            return ApplicationDataValidationResult.MessagePassedValidationCheck;
        }

        private bool IsNullOrEmptyOrWhitespace(string? input)
        {
            return string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input);
        }

        private bool IsAllCharIsLetters(string input)
        {
            foreach (char c in input)
            {
                if (!(char.IsLetter(c) || char.IsWhiteSpace(c)))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsFirstCharIsLetters(string input)
        {
            return char.IsLetter(input[0]);
        }
    }
}
