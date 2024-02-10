using MediatR;
using PermitRequest.Application.Factories;
using PermitRequest.Application.Specifications;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Events;
using PermitRequest.Domain.Services;
using PermitRequest.Infrastructure.EntityFramework.Services;

namespace PermitRequest.Application.DomainEventHandlers
{
    public class LeaveRequestCreatedEventHandler : INotificationHandler<LeaveRequestCreatedEvent>
    {

        private readonly ICumulativeLeaveRequestRepository _cumulativeRepository;
        private readonly ILeaveRequestRepository _leaveRepository;
        private readonly IAdUserRepository _userRepository;
        public LeaveRequestCreatedEventHandler(ICumulativeLeaveRequestRepository cumulativeRepository, ILeaveRequestRepository leaveRepository, IAdUserRepository userRepository)
        {
            _cumulativeRepository = cumulativeRepository;
            _leaveRepository = leaveRepository;
            _userRepository = userRepository;
        }
        public async Task Handle(LeaveRequestCreatedEvent notification, CancellationToken cancellationToken)
        {
            var userEntity = notification.AdUser;

            var leaveEntity = notification.AdUser.LeaveRequests.FirstOrDefault();

            var userAll = await _userRepository.ListAsync(cancellationToken);

            var resultFactory = WorkflowFactory.Workflows(userEntity.UserType, leaveEntity.LeaveType, userAll).Create();

            leaveEntity.SetWorkflowStatus(resultFactory.Item1);
            leaveEntity.SetAssignedUserId(resultFactory.Item2);

            await _leaveRepository.SaveChangesAsync(cancellationToken);

            var filter = new CumulativeLeaveSpec(leaveEntity);

            var cumulativeEntity = await _cumulativeRepository.SingleOrDefaultAsync(filter);

            var entity = CumulativeLeaveRequest.CreateFactory(cumulativeEntity, leaveEntity);

            if (cumulativeEntity != null)
                await _cumulativeRepository.UpdateAsync(entity);
            else
                await _cumulativeRepository.AddAsync(entity);

            await Task.CompletedTask;
        }
    }
}
