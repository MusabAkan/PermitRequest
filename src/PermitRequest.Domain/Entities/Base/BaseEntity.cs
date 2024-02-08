using Ardalis.SharedKernel;

namespace PermitRequest.Domain.Entities.Base
{
    public abstract class BaseEntity : EntityBase
    {
        public new Guid Id { get; set; }
    }
}
