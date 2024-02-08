using Ardalis.SharedKernel;
using PermitRequest.Domain.Extensions;

namespace PermitRequest.Domain.ValueObjets
{
    public class BetweenDate : ValueObject
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string StartDateStr => StartDate.ToString("dd.MM.yyyy");
        public string EndDateStr => EndDate.ToString("dd.MM.yyyy");
        public int Year => StartDate.Year;
        public int TotalWorkHours
        {
            get
            {
                int totalWorkHours = 0;
                const int workHoursPerDay = 8;
                DateTime currentDate = StartDate;
                while (currentDate <= EndDate)
                {
                    if (currentDate.DayOfWeek != DayOfWeek.Sunday)
                        totalWorkHours += workHoursPerDay;
                    currentDate = currentDate.AddDays(1);
                }
                return totalWorkHours;
            }
        }
        public BetweenDate(DateTime startDate, DateTime endDate)
        {
            if (startDate.Date >= endDate.Date)
                throw new ExceptionMessage("Başlangıç tarih bitiş tarihden büyük yada eşit olmamalıdır..");

            StartDate = startDate;
            EndDate = endDate;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return StartDate;
            yield return EndDate;
        }
    }
}
