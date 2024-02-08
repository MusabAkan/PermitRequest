using MediatR;
using PermitRequest.Application.Features.Factories;
using PermitRequest.Application.Specifications;
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
                      
            var userId = notification.LeaveRequest.CreatedById;
            var levaeType = notification.LeaveRequest.LeaveType;
            var year =  notification.LeaveRequest.BetweenDates.Year; 
            var total = notification.LeaveRequest.BetweenDates.TotalWorkHours;
             
            var exists = await _repository.FirstOrDefaultAsync(new CumulativeLeaveSpec(userId, levaeType, year));
 
            //var entity = CumulativeLeaveRequestFactory.Create(exists, userId, levaeType, total, year);       

            //if (exists is not null)
            //    await _repository.UpdateAsync(entity);
            //else
            //    await _repository.AddAsync(entity);

            await Task.CompletedTask;
        }
    }
}
