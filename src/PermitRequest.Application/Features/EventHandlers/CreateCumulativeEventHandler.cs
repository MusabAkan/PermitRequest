using MediatR;
using PermitRequest.Application.Features.Events;
using PermitRequest.Application.Features.Factories;
using PermitRequest.Application.Specifications;
using PermitRequest.Infrastructure.EntityFramework.Services;

namespace PermitRequest.Application.Features.EventHandlers
{
    public class CreateCumulativeEventHandler : INotificationHandler<CreateCumulativeEvent>
    {
      
        private readonly ICumulativeLeaveRequestRepository _repository;

        public CreateCumulativeEventHandler(ICumulativeLeaveRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateCumulativeEvent notification, CancellationToken cancellationToken)
        {         
                      
            var userId = notification.LeaveRequest.CreatedById;
            var levaeType = notification.LeaveRequest.LeaveType;
            var year =  notification.LeaveRequest.Year; 
            var total = notification.LeaveRequest.TotalWorkHours;

            var exists = await _repository.FirstOrDefaultAsync(new CumulativeLeaveSpec(userId, levaeType, year));
 
            var entity = CumulativeLeaveRequestFactory.CreateCumulativeLeaveRequest(exists, userId, levaeType, total, year);       

            if (exists is not null)
                await _repository.UpdateAsync(entity);
            else
                await _repository.AddAsync(entity);

            await Task.CompletedTask;
        }
    }
}
