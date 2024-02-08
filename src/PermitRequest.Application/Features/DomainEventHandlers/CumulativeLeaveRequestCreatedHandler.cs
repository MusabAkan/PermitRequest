using MediatR;
using PermitRequest.Application.Specifications;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Events;
using PermitRequest.Infrastructure.EntityFramework.Services;

namespace PermitRequest.Application.Features.EventHandlers
{
    public class CumulativeLeaveRequestCreatedHandler : INotificationHandler<CumulativeLeaveRequestCreatedEvent>
    {

        private readonly ICumulativeLeaveRequestRepository _repository;
        public CumulativeLeaveRequestCreatedHandler(ICumulativeLeaveRequestRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(CumulativeLeaveRequestCreatedEvent notification, CancellationToken cancellationToken)
        {
            var leaveRequestId = notification.LeaveRequest.Id;
            var userId = notification.LeaveRequest.CreatedById;
            var levaeType = notification.LeaveRequest.LeaveType;
            var year = notification.LeaveRequest.BetweenDates.Year;
            var total = notification.LeaveRequest.BetweenDates.TotalWorkHours;

            var filter = new CumulativeLeaveSpec(userId, levaeType, year);

            var exists = await _repository.SingleOrDefaultAsync(filter);

            var entity = CumulativeLeaveRequest.CreateFactory(exists, userId, levaeType, total, year, leaveRequestId);

            if (exists is not null)
                await _repository.UpdateAsync(entity);
            else
                await _repository.AddAsync(entity);

            await Task.CompletedTask;
        }
    }
}
