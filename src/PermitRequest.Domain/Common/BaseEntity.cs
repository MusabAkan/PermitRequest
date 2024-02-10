using Ardalis.SharedKernel;

namespace PermitRequest.Domain.Common
{
    public abstract class BaseEntity : EntityBase
    {
        public new Guid Id { get; set; }
    }
}
