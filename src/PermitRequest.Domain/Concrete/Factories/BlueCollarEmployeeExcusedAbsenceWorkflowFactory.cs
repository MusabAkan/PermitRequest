﻿using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Interfaces;

namespace PermitRequest.Domain.Concrete.Factories
{
    public class BlueCollarEmployeeExcusedAbsenceWorkflowFactory : IWorkflowFactory
    {
        (Workflow, string) IWorkflowFactory.CreateWorkflow() => (Workflow.Pending, "Manager");
    }
}