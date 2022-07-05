using Resources.Enums;

namespace Resources.Extensions
{
    public static class TransformationsApplicationStatus
    {
        public static string TranslateApplicationStatusToDisplayString(this ApplicationStatus status)
        {
            return status switch
            {
                ApplicationStatus.Indeterminate => "Неопределённые",
                ApplicationStatus.Initial => "Новые",
                ApplicationStatus.Being => "В работе",
                ApplicationStatus.Completed => "Завершенные",
                ApplicationStatus.Rejected => "Отклоненные",
                ApplicationStatus.Canceled => "Отмененные",
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }
    }
}
