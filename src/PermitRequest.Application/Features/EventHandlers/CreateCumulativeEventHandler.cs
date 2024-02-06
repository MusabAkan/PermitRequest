using Ardalis.SharedKernel;
using MediatR;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Events;
using PermitRequest.Domain.Specifications;

namespace PermitRequest.Application.Features.EventHandlers
{
    public class CreateCumulativeEventHandler : INotificationHandler<CreateCumulativeEvent>
    {
      
        private readonly IRepository<CumulativeLeaveRequest> _repository;

        public CreateCumulativeEventHandler(IRepository<CumulativeLeaveRequest> repository)
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
 
            var entity = CumulativeLeaveRequest.CreateCumulativeLeaveRequestFactory(exists, userId, levaeType, total, year);       

            if (exists is not null)
                await _repository.UpdateAsync(entity);
            else
                await _repository.AddAsync(entity);

            await Task.CompletedTask;
        }
    }
}
