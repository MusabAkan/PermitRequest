using Ardalis.SharedKernel;
using MediatR;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Events;

namespace PermitRequest.Application.EventHandlers
{
    public class CreateLeaveRequestEventHandlers : INotificationHandler<CreateLeaveRequestEvent>
    {
        IRepository<AdUser> _userRepository;

        public CreateLeaveRequestEventHandlers(IRepository<AdUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(CreateLeaveRequestEvent notification, CancellationToken cancellationToken)
        {
            var a = notification.LeaveRequest;
            var result = await _userRepository.ListAsync();

            await Task.CompletedTask;
        }
    }
}
