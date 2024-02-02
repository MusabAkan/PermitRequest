using PermitRequest.Domain.Enums;

namespace PermitRequest.Application.Dtos
{
    public class RequestRecordDto
    {
        public string UserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public LeaveType LeaveType { get; set; }
    }
}
