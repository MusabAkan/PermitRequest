using MediatR;
using PermitRequest.Domain.Events;

namespace PermitRequest.Application.EventHandlers
{
    public class CreateLeaveRequestEventHandlers : INotificationHandler<CreateLeaveRequestEvent>
    {
        public Task Handle(CreateLeaveRequestEvent notification, CancellationToken cancellationToken)
        {

            Console.WriteLine("NAber");
              return Task.CompletedTask;
              
        }
    }
}
