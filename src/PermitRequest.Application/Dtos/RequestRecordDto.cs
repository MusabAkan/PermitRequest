using PermitRequest.Domain.Enums;


namespace PermitRequest.Application.Dtos
{
    public record RequestRecordDto(string UserId, DateTime StartTime, DateTime EndTime, LeaveType LeaveType, string reason);
}
