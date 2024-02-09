
using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Services;

namespace PermitRequest.Application.Concrete
{
    public class WhiteCollarEmployeeWorkflowFactory : IWorkflowFactory
    {
        (Workflow, string) IWorkflowFactory.CreateWorkflow() => (Workflow.Pending, "EmployeeManagerCase");
    }
}

