﻿using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Interfaces;

namespace PermitRequest.Domain.Factories
{
    public class WhiteCollarEmployeeWorkflowFactory : IWorkflowFactory
    {
        (Workflow, string) IWorkflowFactory.CreateWorkflow() => (Workflow.Pending, "EmployeeManagerCase");
    }
}
