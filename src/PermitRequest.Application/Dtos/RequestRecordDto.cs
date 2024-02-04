using PermitRequest.Domain.Enums;
namespace PermitRequest.Application.DTOs
{
    public record RequestRecordDto(string UserId, string  StartDate, string EndDate, int LeaveType, string reason);
}
