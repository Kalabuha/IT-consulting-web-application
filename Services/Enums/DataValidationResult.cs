namespace Services.Enums
{
    public enum DataValidationResult : byte
    {
        NameIsEmpty = 1,
        NameIsTooShort,
        NameIsTooLong,
        NameContainsInvalidCharacters,
        NamePassedValidationCheck,

        EmailIsEmpty,
        EmailIsTooShort,
        EmailIsTooLong,
        EmailInvalidFirstCharacter,
        EmailPassedValidationCheck,

        MessageIsEmpty,
        MessageIsTooShort,
        MessageIsTooLong,
        MessagePassedValidationCheck
    }
}
