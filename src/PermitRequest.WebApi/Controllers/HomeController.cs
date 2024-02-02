using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PermitRequest.Application.Commons;
using PermitRequest.Application.Dtos;

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
        public async Task<ActionResult> CreateRequestRecord(RequestRecordDto request)
        {

            var command = _mapper.Map<CreateRequestRecordCommand>(request);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
