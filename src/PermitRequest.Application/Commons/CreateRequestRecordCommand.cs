using Ardalis.Result;
using Ardalis.SharedKernel;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Specifications;

namespace PermitRequest.Application.Commons
{

    public record CreateRequestRecordCommand(string UserId, DateTime StartDate, DateTime EndDate, LeaveType LeaveType, string Reason) : ICommand<Result<bool>>;

    public class CreateRequestRecordCommandHandler : ICommandHandler<CreateRequestRecordCommand, Result<bool>>
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

            var exists = await adUserRepository.FirstOrDefaultAsync(new AdUserSpec(userId));

            if (exists is null)
                return Result.Error("Sistemde kullanıcı bulunamadı");

            var leaveRequst = LeaveRequest.CreateLeaveRequestFactory(exists, request.StartDate, request.EndDate, request.LeaveType, request.Reason);

            await leaveRequestRepository.AddAsync(leaveRequst, cancellationToken);


            return Result.Success(true, "İşlem  tamamlandı");

        }

    }
}
