using Services.Enums;
using Services.Interfaces;
using Services.ServiceResources;
using Resources.Models;

namespace Services
{
    internal class DataValidationService : IDataValidationService
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

        private string? GetErrorMessageInName(DataValidationResult nameValidationCheckResult)
        {
            switch (nameValidationCheckResult)
            {
                case DataValidationResult.NameIsEmpty:
                    return "Поле с вашим именем не заполнено";
                case DataValidationResult.NameIsTooShort:
                    return $"Имя должно быть не короче {LowerNameLengthLimit} символов";
                case DataValidationResult.NameIsTooLong:
                    return $"Имя должно быть не длиннее {UpperNameLengthLimit} символов";
                case DataValidationResult.NameContainsInvalidCharacters:
                    return "Имя должно содержать только буквенные символы";
                case DataValidationResult.NamePassedValidationCheck:
                    return null;

                default:
                    throw new ApplicationException(UnknownTestResult);
            }
        }

        private string? GetErrorMessageInEmail(DataValidationResult emailValidationCheckResult)
        {
            switch (emailValidationCheckResult)
            {
                case DataValidationResult.EmailIsEmpty:
                    return "Поле с вашим e-mail не заполнено";
                case DataValidationResult.EmailIsTooShort:
                    return $"Допускается не менее {LowerEmailLengthLimit} символов";
                case DataValidationResult.EmailIsTooLong:
                    return $"Допускается не более {UpperEmailLengthLimit} символов";
                case DataValidationResult.EmailInvalidFirstCharacter:
                    return "Не допускается вводить спец. символ в начале e-mail";
                case DataValidationResult.EmailPassedValidationCheck:
                    return null;

                default:
                    throw new ApplicationException(UnknownTestResult);
            }
        }

        private string? GetErrorMessageInMessage(DataValidationResult messageValidationCheckResult)
        {
            switch (messageValidationCheckResult)
            {
                case DataValidationResult.MessageIsEmpty:
                    return "Поле с сообщением не заполнено";
                case DataValidationResult.MessageIsTooShort:
                    return $"Вы ввели слишком короткое сообщение. Допускается не менее {LowerMessageLengthLimit} символов";
                case DataValidationResult.MessageIsTooLong:
                    return $"Вы ввели слишком длинное сообщение. Допускается не более {UpperMessageLengthLimit} символов";
                case DataValidationResult.MessagePassedValidationCheck:
                    return null;

                default:
                    throw new ApplicationException(UnknownTestResult);
            }
        }

        private DataValidationResult NameValidationCheck(string? name)
        {
            if (IsNullOrEmptyOrWhitespace(name))
            {
                return DataValidationResult.NameIsEmpty;
            }

            var nameLength = name!.Length;
            if (nameLength < LowerNameLengthLimit)
            {
                return DataValidationResult.NameIsTooShort;
            }
            else if (nameLength > UpperNameLengthLimit)
            {
                return DataValidationResult.NameIsTooLong;
            }

            if (!IsAllCharIsLetters(name))
            {
                return DataValidationResult.NameContainsInvalidCharacters;
            }

            return DataValidationResult.NamePassedValidationCheck;
        }

        private DataValidationResult EmailValidationCheck(string? email)
        {
            if (IsNullOrEmptyOrWhitespace(email))
            {
                return DataValidationResult.EmailIsEmpty;
            }

            var emailLength = email!.Length;
            if (emailLength < LowerEmailLengthLimit)
            {
                return DataValidationResult.EmailIsTooShort;
            }
            else if (emailLength > UpperEmailLengthLimit)
            {
                return DataValidationResult.EmailIsTooLong;
            }

            if (!IsFirstCharIsLetters(email))
            {
                return DataValidationResult.EmailInvalidFirstCharacter;
            }

            return DataValidationResult.EmailPassedValidationCheck;
        }

        private DataValidationResult MessageValidationCheck(string? message)
        {
            if (IsNullOrEmptyOrWhitespace(message))
            {
                return DataValidationResult.MessageIsEmpty;
            }

            var messageLength = message!.Length;
            if (messageLength < LowerMessageLengthLimit)
            {
                return DataValidationResult.MessageIsTooShort;
            }
            else if (messageLength > UpperMessageLengthLimit)
            {
                return DataValidationResult.MessageIsTooLong;
            }

            return DataValidationResult.MessagePassedValidationCheck;
        }

        private bool IsNullOrEmptyOrWhitespace(string? input)
        {
            return string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input);
        }

        private bool IsAllCharIsLetters(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsLetter(c))
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
