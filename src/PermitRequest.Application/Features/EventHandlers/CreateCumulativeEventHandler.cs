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
            
            var startDate = notification.LeaveRequest.StartDate;
            var endDate = notification.LeaveRequest.EndDate;           
            var userId = notification.LeaveRequest.CreatedById;
            var levaeType = notification.LeaveRequest.LeaveType;

            var exists = await _repository.FirstOrDefaultAsync(new CumulativeLeaveSpec(userId, levaeType, startDate.Year));

            var entity = CumulativeLeaveRequest.CreateCumulativeLeaveRequestFactory(exists, userId, levaeType, startDate, endDate);       

            if (exists is not null)
                await _repository.UpdateAsync(entity);
            else
                await _repository.AddAsync(entity);

            await Task.CompletedTask;
        }
    }
}
