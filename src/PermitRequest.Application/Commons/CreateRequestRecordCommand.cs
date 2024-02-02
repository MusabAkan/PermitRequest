using Ardalis.Result;
using Ardalis.SharedKernel;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Specifications;

namespace PermitRequest.Application.Commons
{

    public record CreateRequestRecordCommand(string UserId, DateTime StartTime, DateTime EndTime, LeaveType LeaveType, string reason) : ICommand<Result<bool>>;

    public class CreateRequestRecordCommandHandler(IRepository<AdUser> repository) : ICommandHandler<CreateRequestRecordCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(CreateRequestRecordCommand request, CancellationToken cancellationToken)
        {
            var exists = await repository.FirstOrDefaultAsync(new AdUserSpec(request.UserId));

            if (exists == null)
                throw new Exception("User kullanıcı bulunamadı");

            LeaveRequest.CreateRequestRecord(exists, request.StartTime, request.EndTime, request.LeaveType, request.reason);

           return Result.Success(true, "Ekleme işlemi tamamlandı.");
        }
    }
}
