namespace PermitRequest.Domain.Extensions
{
    public static class ExtensionHelper
    {
        public static string DateTimeToString(this DateTime dateTime) => dateTime.ToString("dd.MM.yyyy HH:mm");
        public static int TotalWorkHourCalculate(this object _, DateTime startDate, DateTime endDate)
        {
            int totalWorkHours = 0;
            const int work = 8;
            DateTime currentDate = startDate;

            while (currentDate <= endDate)
            {
                if (currentDate.DayOfWeek != DayOfWeek.Sunday)
                    totalWorkHours += work;
                currentDate = currentDate.AddDays(1);
            }
            return totalWorkHours;
        }
    }
}
