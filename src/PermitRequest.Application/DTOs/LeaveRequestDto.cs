namespace PermitRequest.Application.DTOs
{
    public class LeaveRequestDto
    {
        public string ReqFormNumber { get; set; }
        public string FullName { get; set; }
        public string LeaveType { get; set; }
        public string CreateDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int TotalHour { get; set; }
        public string Workflow { get; set; }
    };
}
