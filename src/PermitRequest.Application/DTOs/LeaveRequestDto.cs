namespace PermitRequest.Application.DTOs
{
    public record LeaveRequestDto(string ReqFormNumber, string FullName, string LeaveType, string CreateDate, string StartDate, string EndData, int TotalHour, string Workflow);
}
