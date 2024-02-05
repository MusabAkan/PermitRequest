﻿using Ardalis.Result;
using Ardalis.SharedKernel;
using MediatR;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Specifications;
namespace PermitRequest.Application.Features.Commands
{

    public record CreateRequestRecordCommand(string UserId, DateTime StartDate, DateTime EndDate, LeaveType LeaveType, string Reason) : IRequest<Result<bool>>;

    public class CreateRequestRecordCommandHandler : IRequestHandler<CreateRequestRecordCommand, Result<bool>>
    {
        private readonly IRepository<AdUser> adUserRepository;
        private readonly IRepository<LeaveRequest> leaveRequestRepository;

        public CreateRequestRecordCommandHandler(IRepository<AdUser> adUserRepository, IRepository<LeaveRequest> leaveRequestRepository)
        {
            this.adUserRepository = adUserRepository;
            this.leaveRequestRepository = leaveRequestRepository;
        }

        public async Task<Result<bool>> Handle(CreateRequestRecordCommand request, CancellationToken cancellationToken)
        {

            if (!Guid.TryParse(request.UserId, out Guid userId))
                return Result.Error("Kullanıcı Id'si Guid Tipinde olmalıdır");

            if (request.StartDate.Date >= request.EndDate.Date)
                return Result.Error("Başlangıç tarih bitiş tarihden büyük yada eşit olmamalıdır..");

            var exists = await adUserRepository.FirstOrDefaultAsync(new AdUserSpec(userId), cancellationToken);

            if (exists is null)
                return Result.Error("Sistemde kullanıcı bulunamadı");


            var leaveRequest = LeaveRequest.CreateLeaveRequestFactory(exists, request.StartDate, request.EndDate, request.LeaveType, request.Reason);

            switch (leaveRequest.AssignedUserStr)
            {
                case "ManagerOfManagerCase":
                    leaveRequest.AssignedUserId = adUserRepository.FirstOrDefaultAsync(new AdUserSpec(userId), cancellationToken).GetAwaiter().GetResult().ManagerId;
                    break;
                case "ManagerCase":
                    leaveRequest.AssignedUserId = adUserRepository.FirstOrDefaultAsync(new AdUserSpec(), cancellationToken).GetAwaiter().GetResult().Id;
                    break;
                case "EmployeeManagerCase":
                    leaveRequest.AssignedUserId = userId;
                    break;

                default:
                    leaveRequest.AssignedUserId = null;
                    break;

            }

            await leaveRequestRepository.AddAsync(leaveRequest, cancellationToken);

            return Result.Success(true, "İşlem  tamamlandı");

        }
        //private Guid? ManagerOfManagerCase(Guid userId)
        //{
        //    var result = adUserRepository.FirstOrDefaultAsync(new AdUserSpec(userId)).GetAwaiter().GetResult();
        //    return result?.ManagerId;
        //}
        //private Guid? ManagerCase(Guid userId)
        //{
        //    var result = adUserRepository.FirstOrDefaultAsync(new AdUserSpec()).GetAwaiter().GetResult();
        //    return result?.Id;
        //}
        //private Guid EmployeeManagerCase(Guid userId) => userId;
        //private Guid? NotManagerCase(Guid userId) => null;




    }
}