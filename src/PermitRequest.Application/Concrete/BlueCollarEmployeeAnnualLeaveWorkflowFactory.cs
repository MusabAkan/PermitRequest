﻿using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Interfaces;

namespace PermitRequest.Application.Concrete
{
    public class BlueCollarEmployeeAnnualLeaveWorkflowFactory : IWorkflowFactory
    {
        (Workflow, string) IWorkflowFactory.CreateWorkflow() => (Workflow.Pending, "ManagerOfManagerCase");
    }
}
