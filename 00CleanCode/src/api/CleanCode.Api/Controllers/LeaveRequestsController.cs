using CleanCode.Core.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using CleanCode.Core.Application.Features.LeaveRequest.Commands.ChangeApprovalLeaveRequest;
using CleanCode.Core.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using CleanCode.Core.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
using CleanCode.Core.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using CleanCode.Core.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;
using CleanCode.Core.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanCode.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<LeaveRequestsController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestDto>>> Get()
        {
            var lRequests = await _mediator.Send(new GetLeaveRequestsQuery());
            return Ok(lRequests);
        }

        // GET api/<LeaveRequestsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequestDetailsDto>> Get(int id)
        {
            var lRequest = await _mediator.Send(new GetLeaveRequestDetailsQuery(id));

            return Ok(lRequest);
        }

        // PUT api/<LeaveRequestsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put(UpdateLeaveRequestCommand leaveRequest)
        {
            await _mediator.Send(leaveRequest);

            return NoContent();
        }
        
        // PUT api/<LeaveRequestsController>/CancelRequest/
        [HttpPut]
        [Route("CancelRequest")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> CancelRequest(CancelLeaveRequestCommand cancelLeaveRequest)
        {
            await _mediator.Send(cancelLeaveRequest);

            return NoContent();
        }
        
        // PUT api/<LeaveRequestsController>/UpdateApproval/
        [HttpPut]
        [Route("UpdateApproval")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateApproval(ChangeApprovalLeaveRequestCommand changeApprovalLeaveRequest)
        {
            await _mediator.Send(changeApprovalLeaveRequest);

            return NoContent();
        }

        // POST api/<LeaveRequestsController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Post(CreateLeaveRequestCommand leaveRequest)
        {
            var response = await _mediator.Send(leaveRequest);

            return CreatedAtAction(nameof(Get), new { id = response });
        }


        // DELETE api/<LeaveRequestsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveRequestCommand { Id = id };
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
