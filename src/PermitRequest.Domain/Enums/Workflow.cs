﻿namespace PermitRequest.Domain.Enums
{
    public enum Workflow
    {
        None = 0,
        Pending = 10,
        Approved = 20,
        Rejected = 30,
        Exception = 100
    }
}
