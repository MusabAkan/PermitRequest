using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Interfaces;

namespace PermitRequest.Domain.Concrete.Factories
{
    public class ManagerWorkflowFactory : IWorkflowFactory
    {
        (Workflow, string) IWorkflowFactory.CreateWorkflow() => (Workflow.None, "Empty");
    }
}
