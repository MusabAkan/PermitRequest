namespace PermitRequest.Domain.DTOs
{
    public record GetCumulativeLeaveRequestDto(int skip, int take, string userId, int? year, int leaveType = 0);
}
