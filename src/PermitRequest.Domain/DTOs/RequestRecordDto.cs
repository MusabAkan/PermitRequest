namespace PermitRequest.Domain.DTOs
{
    public record RecordRequestDto(string userId, string startDate, string endDate, int leaveType, string reason);
}
