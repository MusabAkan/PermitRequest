using PermitRequest.Domain.Enums;

namespace PermitRequest.Domain.Interfaces
{
    public interface IWorkflowFactory
    {
        (Workflow, string) CreateWorkflow();
    }
}
