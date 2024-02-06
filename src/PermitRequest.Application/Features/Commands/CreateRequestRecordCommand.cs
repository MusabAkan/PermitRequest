using Ardalis.Result;
using Ardalis.SharedKernel;
using MediatR;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Extensions;
using PermitRequest.Domain.Specifications;
namespace PermitRequest.Application.Features.Commands
{

    public record CreateRequestRecordCommand(string UserId, DateTime StartDate, DateTime EndDate, LeaveType LeaveType, string Reason) : IRequest<Result<Guid>>;

    public class CreateRequestRecordCommandHandler : IRequestHandler<CreateRequestRecordCommand, Result<Guid>>
    {
        private readonly IRepository<AdUser> adUserRepository;
        private readonly IRepository<LeaveRequest> leaveRequestRepository;

        public CreateRequestRecordCommandHandler(IRepository<AdUser> adUserRepository, IRepository<LeaveRequest> leaveRequestRepository)
        {
            this.adUserRepository = adUserRepository;
            this.leaveRequestRepository = leaveRequestRepository;
        }

        public async Task<Result<Guid>> Handle(CreateRequestRecordCommand request, CancellationToken cancellationToken)
        {

            if (!Guid.TryParse(request.UserId, out Guid userId))
                throw new ExceptionMessage("Id tipi Guid olmalıdır...");

            var exists = await adUserRepository.FirstOrDefaultAsync(new AdUserSpec(userId), cancellationToken);

            if (exists is null)
                throw new ExceptionMessage("Kullanıcı bulunamadı..");

            var managerId = exists.ManagerId;

            var managerOfManagerId = (await adUserRepository.FirstOrDefaultAsync(new AdUserSpec(), cancellationToken)).Id;

            var leaveRequest = LeaveRequest.CreateLeaveRequestFactory(exists, request.StartDate, request.EndDate, request.LeaveType, request.Reason, managerId, managerOfManagerId);          

            await leaveRequestRepository.AddAsync(leaveRequest, cancellationToken);

            return Result.Success(leaveRequest.Id, "Tüm işlemler tamamlanmıştır.");

        }       
    }
}
