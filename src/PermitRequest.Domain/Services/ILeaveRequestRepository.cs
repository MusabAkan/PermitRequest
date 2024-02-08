using Ardalis.SharedKernel;
using Ardalis.Specification;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Infrastructure.EntityFramework.Services
{
    public interface ILeaveRequestRepository : IRepository<LeaveRequest>
    {
    }
}
