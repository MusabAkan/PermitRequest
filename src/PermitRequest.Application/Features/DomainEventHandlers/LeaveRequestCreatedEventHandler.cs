using MediatR;
using PermitRequest.Application.Concrete;
using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Events;
using PermitRequest.Domain.Extensions;
using PermitRequest.Domain.Services;
using PermitRequest.Infrastructure.EntityFramework.Repositories;

namespace PermitRequest.Application.Features.EventHandlers
{
    public class LeaveRequestCreatedEventHandler : INotificationHandler<LeaveRequestCreatedEvent>
    {

        private readonly ICumulativeLeaveRequestRepository _cumulativeLeaveRequestRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public LeaveRequestCreatedEventHandler(ICumulativeLeaveRequestRepository cumulativeLeaveRequestRepository, ILeaveRequestRepository leaveRequestRepository)
        {
            _cumulativeLeaveRequestRepository = cumulativeLeaveRequestRepository;
            _leaveRequestRepository = leaveRequestRepository;
        }

        public async Task Handle(LeaveRequestCreatedEvent notification, CancellationToken cancellationToken)
        {
            var userEntity = notification.AdUser;
            var leaveEntity = notification.AdUser.LeaveRequests.FirstOrDefault();

            static IWorkflowFactory CreateWorkflowFactory(UserType userType, LeaveType leaveType)
            {
                switch (userType)
                {
                    case UserType.WhiteCollarEmployee:
                        return new WhiteCollarEmployeeWorkflowFactory();
                    case UserType.BlueCollarEmployee when leaveType == LeaveType.AnnualLeave:
                        return new BlueCollarEmployeeAnnualLeaveWorkflowFactory();
                    case UserType.BlueCollarEmployee when leaveType == LeaveType.ExcusedAbsence:
                        return new BlueCollarEmployeeExcusedAbsenceWorkflowFactory();
                    case UserType.Manager:
                        return new ManagerWorkflowFactory();
                    default:
                        throw new ExceptionMessage("WorkFlowFactory hata aldı!!");

                }
            }

            var resultFactory = CreateWorkflowFactory(userEntity.UserType, leaveEntity.LeaveType).CreateWorkflow();

            leaveEntity.SetWorkFlow(resultFactory.Item1);

            await _leaveRequestRepository.SaveChangesAsync();


            //var leaveRequestId = notification.LeaveRequest.Id;
            //var userId = notification.LeaveRequest.CreatedById;
            //var levaeType = notification.LeaveRequest.LeaveType;
            //var year = notification.LeaveRequest.BetweenDates.Year;
            //var total = notification.LeaveRequest.BetweenDates.TotalWorkHours;

            //var filter = new CumulativeLeaveSpec(userId, levaeType, year);

            //var exists = await _repository.SingleOrDefaultAsync(filter);

            //var entity = CumulativeLeaveRequest.CreateFactory(exists, userId, levaeType, total, year, leaveRequestId);

            //if (exists is not null)
            //    await _repository.UpdateAsync(entity);
            //else
            //    await _repository.AddAsync(entity);

            await Task.CompletedTask;
        }
    }
}
