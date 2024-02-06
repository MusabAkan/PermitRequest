using MediatR;
using PermitRequest.Application.Features.Events;
using PermitRequest.Application.Features.Factories;
using PermitRequest.Application.Specifications;
using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Extensions;
using PermitRequest.Infrastructure.EntityFramework.Services;


namespace PermitRequest.Application.Features.EventHandlers
{
    public class CreateNotificationEventHandler : INotificationHandler<CreateNotificationEvent>
    {
        private readonly ICumulativeLeaveRequestRepository _repositoryCumulative;
        private readonly INotificationRepository _notificationRepository;

        public CreateNotificationEventHandler(ICumulativeLeaveRequestRepository repositoryCumulative, INotificationRepository notificationRepository)
        {
            _repositoryCumulative = repositoryCumulative;
            _notificationRepository = notificationRepository;
        }

        public async Task Handle(CreateNotificationEvent notification, CancellationToken cancellationToken)
        {

            var userId = notification.CumulativeLeaveRequest.UserId;
            var leaveType = notification.CumulativeLeaveRequest.LeaveTypeId;
            var year = notification.CumulativeLeaveRequest.Year;

            var existsEntity = _repositoryCumulative.FirstOrDefaultAsync(new CumulativeLeaveSpec(userId, leaveType, year)).Result;

            if (existsEntity != null)
            {

                var leaveRequests = existsEntity.User.LeaveRequests;

                var entity = NotificationRequestFactory.CreateNotificationRequest(existsEntity.Id, existsEntity.UserId);

                var totalDayCurrrent = ((int)existsEntity.TotalHours / 8);

                // İzin süresi sınırları kontrolü
                if (existsEntity.LeaveTypeId == LeaveType.AnnualLeave && totalDayCurrrent > 14)
                {
                    // %10 fazla izin alındığında exception fırlat ve bildirim gönder
                    if (totalDayCurrrent > 14 * 1.10)
                    {
                        entity.Message = "AnnualLeave izin süresi aşıldı.";
                        await _notificationRepository.AddAsync(entity);
                        
                        return;
                    }

                    entity.Message = "AnnualLeave izin süresi %10 fazla alındı.";
                    await _notificationRepository.AddAsync(entity);

                }
                else if (existsEntity.LeaveTypeId == LeaveType.ExcusedAbsence && totalDayCurrrent > 5)
                {
                    // %20 fazla izin alındığında exception fırlat ve bildirim gönder
                    if (totalDayCurrrent > 5 * 1.20)
                    {
                        entity.Message = "ExcusedAbsence izin süresi aşıldı.";
                        await _notificationRepository.AddAsync(entity);
                        return;
                    }

                    entity.Message = "ExcusedAbsence izin süresi %20 fazla alındı.";
                    await _notificationRepository.AddAsync(entity);

                }

                // Her izin tipi için müsade edilen gün sayısının %80'i kullanıldığında  
                var allowedDays = existsEntity.LeaveTypeId == LeaveType.AnnualLeave ? 14 : 5;
                var threshold = allowedDays * 0.80;
                if (totalDayCurrrent >= threshold)
                {
                    entity.Message = $"{existsEntity.LeaveTypeId} izin süresinin %80'i kullanıldı.";
                    await _notificationRepository.AddAsync(entity);
                }
            }
            else
                throw new ExceptionMessage("Kümülatif tablosuna eklenmemiş yada silinmiştir!!");

           await Task.CompletedTask;

        }
    }
}
