using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Services;

namespace PermitRequest.Application.Concrete
{
    public class ManagerWorkflowFactory : IWorkflowFactory
    {
        (Workflow, string) IWorkflowFactory.CreateWorkflow() => (Workflow.None, "NotManagerCase");
    }
}
