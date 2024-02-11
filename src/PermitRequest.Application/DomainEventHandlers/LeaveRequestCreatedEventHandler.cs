using Ardalis.SharedKernel;
using MediatR;
using PermitRequest.Application.Factories;
using PermitRequest.Application.Specifications;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Events;

namespace PermitRequest.Application.DomainEventHandlers
{
    public class LeaveRequestCreatedEventHandler : INotificationHandler<LeaveRequestCreatedEvent>
    {
        private readonly IRepository<CumulativeLeaveRequest> _cumulativeRepository;
        private readonly IRepository<LeaveRequest> _leaveRepository;
        private readonly IRepository<AdUser> _userRepository;
        public LeaveRequestCreatedEventHandler(IRepository<CumulativeLeaveRequest> cumulativeRepository, IRepository<LeaveRequest> leaveRepository, IRepository<AdUser> userRepository)
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

            leaveEntity.SetWorkflowStatus(resultFactory.Workflow);
            leaveEntity.SetAssignedUserId(resultFactory.UserId);

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
