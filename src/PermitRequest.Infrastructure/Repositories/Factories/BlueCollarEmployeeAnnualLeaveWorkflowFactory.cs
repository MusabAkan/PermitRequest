using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Services;

namespace PermitRequest.Application.Concrete
{
    public class BlueCollarEmployeeAnnualLeaveWorkflowFactory : IWorkflowFactory
    {
        (Workflow, string) IWorkflowFactory.CreateWorkflow() => (Workflow.Pending, "ManagerOfManagerCase");
    }
}

