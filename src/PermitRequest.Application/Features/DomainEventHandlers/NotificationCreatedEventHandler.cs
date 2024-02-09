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
        public NotificationCreatedEventHandler(INotificationRepository notificationRepository, ILeaveRequestRepository leaveRequestRepository)
        {           
            _notificationRepository = notificationRepository;
            _leaveRequestRepository = leaveRequestRepository;
        }

        public async Task Handle(NotificationCreatedEvent notification, CancellationToken cancellationToken)
        {
            var cumulativetEntity = notification.CumulativeLeaveRequest;

            var leaveEntity = cumulativetEntity.User.LeaveRequests.FirstOrDefault();

            var notificationEntity = Notification.CreateFactory(cumulativetEntity.Id, cumulativetEntity.UserId);

            var totalDayCurrrent = ((int)cumulativetEntity.TotalHours / 8);

            // Her izin tipi için müsade edilen gün sayısının %80'i kullanıldığında  
            var allowedDays = cumulativetEntity.LeaveTypeId == LeaveType.AnnualLeave ? 14 : 5;
            var threshold = allowedDays * 0.80;

            // İzin süresi sınırları kontrolü
            if (cumulativetEntity.LeaveTypeId == LeaveType.AnnualLeave && totalDayCurrrent > 14)
            {
                // %10 fazla izin alındığında exception fırlat ve bildirim gönder
                if (totalDayCurrrent > 14 * 1.10)
                {
                    notificationEntity.Message = "AnnualLeave izin süresi aşıldı.";    
                    leaveEntity.WorkflowStatus = Workflow.Exception;
                    leaveEntity.LastModifiedById = leaveEntity.AssignedUserId;    
                }
                else
                {
                    notificationEntity.Message = "AnnualLeave izin süresi %10 fazla alındı.";            
                }

            }
            else if (cumulativetEntity.LeaveTypeId == LeaveType.ExcusedAbsence && totalDayCurrrent > 5)
            {
                // %20 fazla izin alındığında exception fırlat ve bildirim gönder
                if (totalDayCurrrent > 5 * 1.20)
                {
                    notificationEntity.Message = "ExcusedAbsence izin süresi aşıldı.";
                

                    leaveEntity.WorkflowStatus = Workflow.Exception;
                    leaveEntity.LastModifiedById = leaveEntity.AssignedUserId;
                  
                    return;
                }

                notificationEntity.Message = "ExcusedAbsence izin süresi %20 fazla alındı.";
                await _notificationRepository.AddAsync(notificationEntity);
                

            }       

            else if (totalDayCurrrent >= threshold)
                notificationEntity.Message = $"{cumulativetEntity.LeaveTypeId} izin süresinin %80'i kullanıldı.";    

            await _notificationRepository.AddAsync(notificationEntity);
            await _leaveRequestRepository.SaveChangesAsync();

            await Task.CompletedTask;
        }
    }
}
