using PermitRequest.Domain.Enums;

namespace PermitRequest.Domain.Services
{
    public interface IWorkflowFactory
    {
        (Workflow, string) CreateWorkflow();
    }
}
