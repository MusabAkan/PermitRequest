using Ardalis.SharedKernel;
using MediatR;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Events;

namespace PermitRequest.Application.DomainEventHandlers
{
    public class NotificationCreatedEventHandler : INotificationHandler<NotificationCreatedEvent>
    {
        private readonly IRepository<Notification> _notificationRepository;
        private readonly IRepository<LeaveRequest> _leaveRequestRepository;
        private readonly IRepository<CumulativeLeaveRequest> _cumulativeRepository;
        public NotificationCreatedEventHandler(IRepository<Notification> notificationRepository, IRepository<LeaveRequest> leaveRequestRepository, IRepository<CumulativeLeaveRequest> cumulativeRepository)
        {
            _notificationRepository = notificationRepository;
            _leaveRequestRepository = leaveRequestRepository;
            _cumulativeRepository = cumulativeRepository;
        }
        public async Task Handle(NotificationCreatedEvent notification, CancellationToken cancellationToken)
        {
            const int Works = 8;
            const int AnnualLeaveHour = 14;
            const int ExcusedAbsenceHour = 5;

            var cumulativetEntity = notification.LeaveRequest.CreatedBy.CumulativeLeaveRequests.FirstOrDefault();
            var leaveEntity = notification.LeaveRequest;
            var managerId = notification.LeaveRequest.CreatedBy.ManagerId;
            var leaveType = leaveEntity.LeaveType;
            var leaveWorkHour = leaveEntity.BetweenDates.TotalWorkHours;


            var totalWorkHour = cumulativetEntity.TotalHours;
            var totalWorkDay = (int)totalWorkHour / Works;

            int leaveDurationDay = leaveType is LeaveType.AnnualLeave ? AnnualLeaveHour : ExcusedAbsenceHour;
            int leaveDurationHour = leaveDurationDay * Works;

            int rate80 = (int)(leaveDurationDay * Works * 0.10);

            double rate = leaveType == LeaveType.AnnualLeave ? 1.10 : 1.20;
            string rateString = leaveType is LeaveType.AnnualLeave ? "%10" : "%20";

            double limit = (int)(leaveDurationDay * rate);

            string timeType = leaveType is LeaveType.AnnualLeave ? "gün" : "saat";

            int remaining = leaveType is LeaveType.AnnualLeave ? (leaveDurationDay - totalWorkDay) : (leaveDurationHour - totalWorkHour);
            int exceeded = leaveType is LeaveType.AnnualLeave ? (totalWorkDay - leaveDurationDay) : (totalWorkHour - leaveDurationHour);

            string messageText;

            if (totalWorkDay > limit)
            {
                messageText = $"{leaveType} süresi {rateString} fazla aşıldı";
                 
                leaveEntity.SetWorkflowStatus(Workflow.Exception);

                if(managerId != null)
                    leaveEntity.SetLastModifiedById(managerId);

                await _leaveRequestRepository.SaveChangesAsync(); 

                cumulativetEntity.SetTotalHours(leaveWorkHour, "-");

                await _cumulativeRepository.SaveChangesAsync();
            }
            else
            {
                if (totalWorkDay == rate80)
                    messageText = $"{leaveType} %80 kullanılmıştır";
                else if (totalWorkDay < leaveDurationDay)
                    messageText = $"Kalan {leaveType} {remaining} {timeType}";
                else if (totalWorkDay > leaveDurationDay)
                    messageText = $"Aşılan {leaveType} {exceeded} {timeType}";
                else
                    messageText = $"{leaveType} hepsi kullanılmıştır";
            }


            var notificationEntity = Notification.CreateFactory(cumulativetEntity.Id, cumulativetEntity.UserId, messageText);

            await _notificationRepository.AddAsync(notificationEntity);

            await Task.CompletedTask;
        }
    }
}
