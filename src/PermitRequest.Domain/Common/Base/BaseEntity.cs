using Ardalis.SharedKernel;
using System.ComponentModel.DataAnnotations.Schema;

namespace PermitRequest.Domain.Common.Base
{
    public abstract class BaseEntity : HasDomainEventsBase
    {
        public Guid Id { get; set; }      
    }
}
