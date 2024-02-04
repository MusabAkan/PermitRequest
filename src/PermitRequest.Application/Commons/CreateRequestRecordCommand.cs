using Ardalis.Result;
using Ardalis.SharedKernel;
using MediatR;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Specifications;

namespace PermitRequest.Application.Commons
{

    public record CreateRequestRecordCommand(string UserId, DateTime StartDate, DateTime EndDate, LeaveType LeaveType, string Reason) : IRequest<Result<bool>>;

    public class CreateRequestRecordCommandHandler : IRequestHandler<CreateRequestRecordCommand, Result<bool>>
    {
        private readonly IRepository<AdUser> repository; 

        public async Task<Result<bool>> Handle(CreateRequestRecordCommand request, CancellationToken cancellationToken)
        {
            var exists = await repository.FirstOrDefaultAsync(new AdUserSpec(request.UserId));

            if (exists == null)
                throw new Exception("User kullanıcı bulunamadı");

            var leaveRequst = LeaveRequest.CreateLeaveRequestFactory(exists, request.StartDate, request.EndDate, request.LeaveType, request.Reason);


            return Result.Success(true, "İşlem  tamamlandı");

        }

    }
}
