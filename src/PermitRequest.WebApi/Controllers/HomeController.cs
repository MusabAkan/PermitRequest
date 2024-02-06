using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PermitRequest.Application.Features.Commands;
using PermitRequest.Application.Features.Queries;
using PermitRequest.Domain.DTOs;

namespace PermitRequest.WebApi.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase

    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public HomeController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        [Route($"api/{nameof(CreateRequestRecord)}")]
        public async Task<ActionResult> CreateRequestRecord([FromBody] RequestRecordDto request)
        {

            var command = _mapper.Map<CreateRequestRecordCommand>(request);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        [Route($"api/{nameof(GetListLeaveRequest)}")]
        public async Task<ActionResult> GetListLeaveRequest(GetListDto request)
        {
            var command = _mapper.Map<GetListLeaveRequestQuery>(request);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        [Route($"api/{nameof(GetListLeaveRequestById)}")]
        public async Task<ActionResult> GetListLeaveRequestById(GetByIdDto request)
        {
            var command = _mapper.Map<GetByIdLeaveRequestQuery>(request);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        [Route($"api/{nameof(GetListCumulativeLeaveRequest)}")]
        public async Task<ActionResult> GetListCumulativeLeaveRequest(GetListDto request)
        {
            var command = _mapper.Map<GetListCumulativeLeaveRequestQuery>(request);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        [Route($"api/{nameof(GetListNotificationRequest)}")]
        public async Task<ActionResult> GetListNotificationRequest(GetListDto request)
        {
            var command = _mapper.Map<GetListNotificationRequestQuery>(request);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
