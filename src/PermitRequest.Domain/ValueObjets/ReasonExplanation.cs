using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using PermitRequest.Domain.Extensions;

namespace PermitRequest.Domain.ValueObjets
{
    public class ReasonExplanation : ValueObject
    {
        public string Reason { get; set; }

        private const int MaxLength = 250;
        public ReasonExplanation(string? reason)
        {
            var result = reason.Trim();

            if (result.Length > MaxLength)
                throw new ExceptionMessage($"Sebep açıklamasındaki karakter sayısı {MaxLength} fazla olamaz..");

            Reason = result;
        }
       
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Reason;
        }
    }
}
