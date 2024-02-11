using Ardalis.Result;
using Ardalis.SharedKernel;
using MediatR;
using PermitRequest.Application.Specifications;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Exceptions;

namespace PermitRequest.Application.Commands
{
    public record CreateRequestRecordCommand(Guid UserId, DateTime StartDate, DateTime EndDate, LeaveType LeaveType, string Reason) : IRequest<Result<Guid>>;

    public class CreateRequestRecordCommandHandler : IRequestHandler<CreateRequestRecordCommand, Result<Guid>>
    {
        private readonly IRepository<AdUser> _adUserRepository;
        private readonly IRepository<LeaveRequest> _leaveRequestRepository;

        public CreateRequestRecordCommandHandler(IRepository<AdUser> adUserRepository, IRepository<LeaveRequest> leaveRequestRepository)
        {
            _adUserRepository = adUserRepository;
            _leaveRequestRepository = leaveRequestRepository;
        }
        public async Task<Result<Guid>> Handle(CreateRequestRecordCommand request, CancellationToken cancellationToken)
        {
            var user = await _adUserRepository.FirstOrDefaultAsync(new AdUserSpec(request.UserId));

            if (user == null)
                throw new ExceptionMessage("Kullanıcı bulunamadı..");

            var leaveRequest = LeaveRequest.CreateFactory(user, request.StartDate, request.EndDate, request.LeaveType, request.Reason);

            await _leaveRequestRepository.AddAsync(leaveRequest);

            return Result.Success(leaveRequest.Id, "Tüm işlemler tamamlanmıştır.");
        }
    }
}
