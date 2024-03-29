﻿using Ardalis.Result;
using Ardalis.SharedKernel;
using AutoMapper;
using PermitRequest.Application.Specifications;
using PermitRequest.Domain.DTOs;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Exceptions;

namespace PermitRequest.Application.Queries
{
    public record GetByIdLeaveRequestQuery(int skip, int take, Guid userid) : IQuery<Result<List<LeaveResponsetDto>>>;

    public class GetByIdLeaveRequestQueryHandler(IRepository<LeaveRequest> _repository, IMapper _mapper) : IQueryHandler<GetByIdLeaveRequestQuery, Result<List<LeaveResponsetDto>>>
    {

        public async Task<Result<List<LeaveResponsetDto>>> Handle(GetByIdLeaveRequestQuery request, CancellationToken cancellationToken)
        {

            var filterSpec = new LeaveRequestFilterPaginatedSpec(request.skip, request.take, request.userid);

            var data = await _repository.ListAsync(filterSpec);

            if (data == null || data.Count == default)
                throw new ExceptionMessage("Veri yok!!");

            var leaveRequests = _mapper.Map<List<LeaveResponsetDto>>(data);

            var count = "Total Data : " + leaveRequests.Count;

            return Result.Success(leaveRequests, count);
        }

    }

}
