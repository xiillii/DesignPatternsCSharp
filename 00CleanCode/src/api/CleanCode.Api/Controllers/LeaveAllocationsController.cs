﻿using CleanCode.Core.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanCode.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveAllocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<LeaveAllocationsController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveAllocationDto>>> Get()
        {
            var lAllocations = await _mediator.Send(new GetLeaveAllocationsQuery());

            return Ok(lAllocations);
        }

        // GET api/<LeaveAllocationsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LeaveAllocationsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LeaveAllocationsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LeaveAllocationsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
