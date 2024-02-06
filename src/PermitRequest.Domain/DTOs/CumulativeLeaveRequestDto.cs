namespace PermitRequest.Domain.DTOs
{
    public class CumulativeLeaveRequestDto
    {
        public string FullName { get; set; }
        public string LeaveType { get; set; }
        public string Year { get; set; }
        public string TotalHour { get; set; }
    }
}
