namespace PermitRequest.Application.DTOs
{
    public record CumulativeLeaveRequestDto(string FullName, string LeaveType, string Year, string TotalHour);
}
