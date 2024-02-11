using PermitRequest.Domain.Enums;

namespace PermitRequest.Domain.DTOs
{
    public record WorkflowFactoryDto(Workflow Workflow, Guid? UserId);
}
