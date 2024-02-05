using Ardalis.SharedKernel;
using MediatR;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Events;

namespace PermitRequest.Application.Features.EventHandlers
{
    public class CreateCumulativeEventHandler : INotificationHandler<CreateCumulativeEvent>
    {
        IRepository<AdUser> _userRepository;

        public CreateCumulativeEventHandler(IRepository<AdUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(CreateCumulativeEvent notification, CancellationToken cancellationToken)
        {
            var a = notification.LeaveRequest;
            var result = await _userRepository.ListAsync();

            await Task.CompletedTask;
        }
    }
}
