using Ardalis.Result;
using MediatR;
using PermitRequest.Application.Features.Factories;
using PermitRequest.Application.Specifications;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Extensions;
using PermitRequest.Infrastructure.EntityFramework.Services;
namespace PermitRequest.Application.Features.Commands
{

    public record CreateRequestRecordCommand(string UserId, DateTime StartDate, DateTime EndDate, LeaveType LeaveType, string Reason) : IRequest<Result<Guid>>;

    public class CreateRequestRecordCommandHandler : IRequestHandler<CreateRequestRecordCommand, Result<Guid>>
    {
        private readonly IAdUserRepository _adUserRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public CreateRequestRecordCommandHandler(IAdUserRepository adUserRepository, ILeaveRequestRepository leaveRequestRepository)
        {
            _adUserRepository = adUserRepository;
            _leaveRequestRepository = leaveRequestRepository;
        }
        public async Task<Result<Guid>> Handle(CreateRequestRecordCommand request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.UserId, out Guid userId))
                throw new ExceptionMessage("Id tipi Guid olmalıdır...");

            var exist = await _adUserRepository.FirstOrDefaultAsync(new AdUserSpec(userId));

            if (exist is null)
                throw new ExceptionMessage("Kullanıcı bulunamadı..");

            var userType = exist.UserType;

            var leaveRequest = LeaveRequest.CreateFactory(userId, request.StartDate, request.EndDate, request.LeaveType, request.Reason, userType);

            await _leaveRequestRepository.AddAsync(leaveRequest);

            return Result.Success(leaveRequest.Id, "Tüm işlemler tamamlanmıştır.");

        }
    }
}
