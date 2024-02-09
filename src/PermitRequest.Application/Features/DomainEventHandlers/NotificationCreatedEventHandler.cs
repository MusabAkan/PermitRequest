using MediatR;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Events;
using PermitRequest.Domain.Services;
namespace PermitRequest.Application.Features.EventHandlers
{
    public class NotificationCreatedEventHandler : INotificationHandler<NotificationCreatedEvent>
    {

        private readonly INotificationRepository _notificationRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ICumulativeLeaveRequestRepository _cumulativeRepository;
        public NotificationCreatedEventHandler(INotificationRepository notificationRepository, ILeaveRequestRepository leaveRequestRepository, ICumulativeLeaveRequestRepository cumulativeRepository)
        {
            _notificationRepository = notificationRepository;
            _leaveRequestRepository = leaveRequestRepository;
            _cumulativeRepository = cumulativeRepository;
        }
        public async Task Handle(NotificationCreatedEvent notification, CancellationToken cancellationToken)
        {
            const int AnnualLeave = 14;
            const int ExcusedAbsence = 5;
            const int Works = 8;
           
            var cumulativetEntity = notification.CumulativeLeaveRequest;
            var leaveType = cumulativetEntity.LeaveTypeId;
            var leaveEntity = notification.LeaveRequest;
            
            var totalWorkHour = leaveEntity.BetweenDates.TotalWorkHours;
            var totalWorkDay = (int)totalWorkHour / Works; 
            var limit = leaveType == LeaveType.AnnualLeave ? AnnualLeave : ExcusedAbsence;
            var totalLimit = limit * (0.10 * Works);

            var notificationEntity = Notification.CreateFactory(cumulativetEntity.Id, cumulativetEntity.UserId);
          

            if (leaveType == LeaveType.AnnualLeave && totalWorkDay > AnnualLeave)
            {
                // %10 fazla izin alındığında exception fırlat ve bildirim gönder
                if (totalWorkDay > AnnualLeave * 1.10)
                {
                    notificationEntity.Message = "AnnualLeave izin süresi aşıldı.";
                    await _notificationRepository.AddAsync(notificationEntity);

                    leaveEntity.SetWorkflowStatus(Workflow.Exception);
                    await _leaveRequestRepository.SaveChangesAsync();

                    //Çünkü reddildiği için kümülatif tap da azaltmak benim düşüncem :)
                    cumulativetEntity.SetTotalHours(totalWorkHour, "-");
                    await _cumulativeRepository.SaveChangesAsync();
                }
                else
                {
                    notificationEntity.Message = "AnnualLeave izin süresi %10 fazla alındı.";
                    await _notificationRepository.AddAsync(notificationEntity);
                }

            }
            else if (leaveType == LeaveType.ExcusedAbsence && totalWorkDay > ExcusedAbsence)
            {
                // %20 fazla izin alındığında exception fırlat ve bildirim gönder
                if (totalWorkDay > ExcusedAbsence * 1.20)
                {
                    notificationEntity.Message = "ExcusedAbsence izin süresi aşıldı.";
                    await _notificationRepository.AddAsync(notificationEntity);

                    leaveEntity.SetWorkflowStatus(Workflow.Exception);
                    await _leaveRequestRepository.SaveChangesAsync();

                    cumulativetEntity.SetTotalHours(totalWorkHour, "-");
                    await _cumulativeRepository.SaveChangesAsync();
                }
                else
                {
                    notificationEntity.Message = "ExcusedAbsence izin süresi %20 fazla alındı.";
                    await _notificationRepository.AddAsync(notificationEntity);
                }

            }
            else if (totalWorkDay >= totalLimit)
            {
                // Her izin tipi için müsade edilen gün sayısının %80'i kullanıldığında  
                notificationEntity.Message = $"{leaveType} izin süresinin %80'i kullanıldı.";
                await _notificationRepository.AddAsync(notificationEntity);
            }

            await Task.CompletedTask;
        }
    }
}
