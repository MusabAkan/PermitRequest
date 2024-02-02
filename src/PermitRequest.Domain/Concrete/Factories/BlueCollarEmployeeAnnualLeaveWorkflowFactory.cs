using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Interfaces;

namespace PermitRequest.Domain.Concrete.Factories
{
    public class BlueCollarEmployeeAnnualLeaveWorkflowFactory : IWorkflowFactory
    {
        (Workflow, string) IWorkflowFactory.CreateWorkflow() => (Workflow.Pending, "ManagerOfManager");
    }
}

