﻿namespace PermitRequest.Domain.DTOs
{
    public record RequestRecordDto(string UserId, string StartDate, string EndDate, int LeaveType, string reason);
}