using PermitRequest.Domain.Enums;

namespace PermitRequest.Infrastructure.Repositories.Factories
{
    public interface IWorkflowFactory
    {
        (Workflow, string) CreateWorkflow();
    }
}
